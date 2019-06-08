$(document).ready(function () {
    $("#btnSaveItemGroup").click(function () {
        if (window.FormData !== undefined) {
            var fileUpload = $("#flGroupImage").get(0);
            var files = fileUpload.files;
            // Create FormData object  
            var fileData = new FormData();
            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }
            // Adding one more key to FormData object  
            fileData.append("GroupName", $("#txtGroupName").val());  
            fileData.append("GroupDesc", $("#txtGroupDesc").val());
            $.ajax({
                url: '/Admin/SaveItemGroup',
                type: "POST",
                cache: false,
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {
                    alert(result);
                },
                error: function (err) {
                    alert(err.statusText);
                }
            }); 
        }
        else {
            alert("FormData is not supported.");
        }  
    })

})
$(function () {
    $('.modal-effect').on('click', function (e) {
        e.preventDefault();
        var effect = $(this).attr('data-effect');
        $('#modalItemGroup').addClass(effect);
        $('#modalItemGroup').modal('show');
    });
    $('#modalItemGroup').on('hidden.bs.modal', function (e) {
        $(this).removeClass(function (index, className) {
            return (className.match(/(^|\s)effect-\S+/g) || []).join(' ');
        });
    });
});