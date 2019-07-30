$(document).ready(function () {
    var username = $("#username");
    var password = $("#password");
    var errorLable = $("#error-lable");
    errorLable.text('');
    $("#submit").click(function (e) {
        e.preventDefault();
        errorLable.text('');
        
        submit();
        
    });

    var submit = function () {
        
        var formdata = JSON.stringify({ username: username.val(), password: password.val() });
        $.ajax({
            type: "POST",
            url: "/login",
            data: formdata,
            contentType: "application/json",
            success: function (result) {
                if (result.status) {
                    window.location.href = result.redirectUrl;
                }
                
            },
            error: function (result) {
                if (result.responseJSON.modelError) {
                    $.each(result.responseJSON.modelError, function (index, value) {
                        if (value.key === "Username") {
                            errorLable.text(value.message);
                        }
                        if (value.key === "Password") {
                            errorLable.text(value.message);
                        }
                        if (value.key !== "Username" && value.key !== "Password") {
                           
                            errorLable.text(value.message);
                        }
                    });
                }
                else {
                    errorLable.text(result.responseJSON.message);
                }
                
            }
        });
    };
   
});
