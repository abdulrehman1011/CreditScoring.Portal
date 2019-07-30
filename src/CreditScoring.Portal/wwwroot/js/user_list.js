var userData = null;
$(document).ready(function () {
    
    //$('.alert').hide();
    var selected = [];
    var btnUpdate = $(".update-user");
    var btnDeactivate = $(".deactivate-user");
    var username = $("#username");
    var password = $("#currnetpassword");
    var newPassword = $("#password");
    var userId = $("#userid");
    var token = $("#token");
    var errorLable = $("#error-lable");
    var scoreList = $("#basic");
   
    errorLable.text('');
    

    var errorField = $("#error-field");
    errorField.text('');
    var btnSubmitUpdate = $("#btn-update-user");

    btnDeactivate.click(function (e) {
        e.preventDefault();
        errorField.text('');
        $('.alert').hide();
        var selectedId = this.id;
        if (selectedId != undefined && selectedId != null) {
            dubmitUserDeactivate(selectedId);
        }
        else {

        }
        
    });
    btnSubmitUpdate.click(function (e) {
        e.preventDefault();
       
        errorLable.text('');
        $("#btn-spin").addClass('fa-spin');
        btnSubmitUpdate.attr("disabled", true);
        btnSubmitUpdate.addClass("disabled");
        submit();
    });
    btnUpdate.click(function () {
       
        userData = this.id;
        userData = JSON.parse(userData);
        username.val(userData.Username);
        userId.val(userData.Id);
        var filtered = $.grep(JSON.parse(userDataModel), function (el) {
            return el.Id == userData.Id;
        });
       
        $('#myModal').modal();
       
        scoreList.multiselect({
            templates: {
                li: '<li><a href="javascript:void(0);"><label class="pl-2"></label></a></li>'
            },
            onChange: function (element, checked) {
                var brands = $('#basic option:selected');
                selected = [];
                $(brands).each(function (index, brand) {
                    console.log($(this).text());
                    selected.push($(this).val());
                });

                console.log(selected);
            }
        });
         $('#basic').multiselect('deselectAll', false);
        $('#basic').multiselect('updateButtonText');
        $.each(filtered, function (key, value) {
            scoreList.multiselect('select', value.ScoreId, true);
            //$('#score-selector option[value=' + value.ScoreId + ']').attr('selected', 'selected');

        });
        
    });
    var dubmitUserDeactivate = function (id) {
        var formdata = JSON.stringify({ Id: id});
        $.ajax({
            type: "POST",
            url: "/deactivate-user",
            data: formdata,
            contentType: "application/json",
            success: function (result) {
                if (result.status) {
                    window.location.reload(true);
                }
            },
            error: function (result) {
               
                if (result.responseJSON.modelError) {
                    $.each(result.responseJSON.modelError, function (index, value) {
                        errorField.text(value.message);
                        return false;
                    });
                }
                else {
                    
                    errorField.text(result.responseJSON.message);
                    
                }

                $('.alert').show();
                $(".alert").fadeTo(3000, 500).slideUp(500, function () {
                    $(".alert").slideUp(500);
                });
            }
        });
    };
    var submit = function () {

        var formdata = JSON.stringify({ Id: userId.val(), username: username.val(), CurrentPassword: password.val(), Password: newPassword.val(), token: token.val(), scorebands: selected });
        $.ajax({
            type: "POST",
            url: "/update-user",
            data: formdata,
            contentType: "application/json",
            success: function (result) {
                if (result.status) {
                    window.location.reload(true);
                }
            },
            error: function (result) {
                $("#btn-spin").removeClass('fa-spin');
                btnSubmitUpdate.removeAttr("disabled");
                btnSubmitUpdate.removeClass("disabled");
                if (result.responseJSON.modelError) {
                    $.each(result.responseJSON.modelError, function (index, value) {
                        errorLable.text(value.message);
                        return false;
                    });
                }
                else {
                    errorLable.text(result.responseJSON.message);
                }

            }
        });
    };
    
});

