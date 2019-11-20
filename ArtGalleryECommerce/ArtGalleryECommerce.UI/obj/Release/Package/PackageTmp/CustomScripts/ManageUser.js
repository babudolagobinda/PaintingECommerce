var app = angular.module("divManageUserApp", ['angularUtils.directives.dirPagination']);

app.controller("divManageUserController", function ($scope, $http) {
    GetAllUserList();


    function GetAllUserList() {
        $http({
            method: 'POST',
            dataType: 'JSON',
            url: '/Admin/GetAllUserList',
            data: {},
        }).then(function (res) {
            if (res.data.length > 0) {
                $scope.dataManageUsers = res.data;
            }
        });
    }
    $scope.IsActiveUser = function (par) {
        var isActiveStatus = "";
        var status = "";
        if (par.IsActive === 1) {
            isActiveStatus = "DeActivate";
            status = 0;
        }
        else {
            isActiveStatus = "Activate";
            status = 1;
        }
        swal({
            title: "Are You Sure ?",
            text: "You Can Be Able To Recover This Imaginary File !",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes " + isActiveStatus + " it!",
            cancelButtonText: "No !",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    $http({
                        method: 'POST',
                        dataType: 'JSON',
                        url: '/Admin/UpdateIsActiveUser',
                        data: { UserId: par.UserId, IsActive: status },
                    }).then(function (res) {
                        if (res.data > 0) {
                            swal(isActiveStatus, "Your imaginary file has been " + isActiveStatus+".", "success");
                            GetAllUserList();
                        }
                        else {
                            swal(isActiveStatus, "Some Error Has been Occoured.", "error");
                        }
                    });

                } else {
                    swal("Cancelled", "Your imaginary file is safe :)", "error");
                }
            });
    };
});