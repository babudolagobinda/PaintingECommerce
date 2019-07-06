$(document).ready(function () {
    $('#btnRegister').click(function () {
        if (userRegisterValidate()) {
            alert('OK');
        }
    });
   
});
function userRegisterValidate() {
    var returnVal = false;
    if ($('#txtName').val() === '') {
        $('#txtName').css('border-color', 'red');
        returnVal = false;
    }
    else {
        $('#txtName').css('border-color', '');
        returnVal = true;
    }
    if ($('#txtEmailId').val() === '') {
        $('#txtEmailId').css('border-color', 'red');
        returnVal = false;
    }
    else {
        var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        var validEmail = filter.test($('#txtEmailId').val());
        if (!validEmail) {
            $('#txtEmailId').css('border-color', 'red');
            returnVal = false;
        }
        else {
            $('#txtEmailId').css('border-color', '');
            returnVal = true;
        }
        
    }
    if ($('#txtPassword').val() === '') {
        $('#txtPassword').css('border-color', 'red');
        returnVal = false;
    }
    else {
        $('#txtPassword').css('border-color', '');
        returnVal = true;
    }
    if ($('#txtConfirmPasswod').val() === '') {
        $('#txtConfirmPasswod').css('border-color', 'red');
        returnVal = false;
    }
    else {
        $('#txtConfirmPasswod').css('border-color', '');
        returnVal = true;
    }
    if ($('#txtMobileNo').val() === '') {
        $('#txtMobileNo').css('border-color', 'red');
        returnVal = false;
    }
    else {
        $('#txtMobileNo').css('border-color', '');
        returnVal = true;
    }
    if ($('#ddlGender').val() === '0') {
        $('#ddlGender').css('border-color', 'red');
        returnVal = false;
    }
    else {
        $('#ddlGender').css('border-color', '');
        returnVal = true;
    }
    return returnVal;
}