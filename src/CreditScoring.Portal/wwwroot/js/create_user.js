$(document).ready(function () {
    
    var selected = [];
    var btnCreate = $("#btn-create");
    var username = $("#username");
    var password = $("#password");
    var confirmPassword = $("#confirmpassword");
    var token = $("#token");
    var errorLable = $("#error-lable");
    errorLable.text('');
    btnCreate.click(function (e) {
        
        e.preventDefault();
       
        $("#btn-spin").addClass('fa-spin');
        btnCreate.attr("disabled", true);
        btnCreate.addClass("disabled");
        submit();
        //setTimeout(function () {
        //    $("#btn-spin").removeClass('fa-spin');
        //    btnCreate.removeAttr("disabled");
        //    btnCreate.removeClass("disabled");
        //}, 5000);
        //alert();
    });
    var submit = function () {

        var formdata = JSON.stringify({ username: username.val(), password: password.val(), confirmpassword: confirmPassword.val(), token: token.val(), scorebandList: selected });
        $.ajax({
            type: "POST",
            url: "/create-user",
            data: formdata,
            contentType: "application/json",
            success: function (result) {
                if (result.status) {
                    window.location.href = "/users";
                }

            },
            error: function (result) {
                $("#btn-spin").removeClass('fa-spin');
                btnCreate.removeAttr("disabled");
                btnCreate.removeClass("disabled");
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

    //dropdown js
    $('#basic').multiselect({
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
});