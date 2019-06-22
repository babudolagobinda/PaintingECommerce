var app = angular.module("divItemGroupApp", ['angularUtils.directives.dirPagination']);

app.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;
            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

app.controller("divItemGroupController", function ($scope, $http) {
    GetAllItemGroup();
    resetItemGroup();
    $scope.resetAll = function () {
        resetItemGroup();
    };
    $scope.SaveItemGroup = function () {
        var file = $scope.GroupImage;
        var fd = new FormData();
        fd.append('GroupImage', file);
        fd.append('GroupId', $scope.GroupId);
        fd.append('GroupName', $scope.GroupName);
        fd.append('GroupDesc', $scope.GroupDesc);
        $http({
            method: 'POST',
            dataType: 'JSON',
            url: '/Admin/SaveItemGroup',
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            if (res.data > 0) {
                GetAllItemGroup();
                resetItemGroup();
                var element = angular.element('#modalItemGroup');
                element.modal('hide');
                $scope.show = true;
            }
        });
    };
    $scope.EditItemGroup = function (par) {
        resetItemGroup();
        var element = angular.element('#modalItemGroup');
        element.modal('show');
        $scope.GroupId = par.GroupId;
        $scope.GroupName = par.GroupName;
        $scope.GroupDesc = par.GroupDesc;

    };
    $scope.DeleteItemGroup = function (par) {
        swal({
            title: "Are You Sure ?",
            text: "You Will Not Be Able To Recover This Imaginary File !",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Delete It !",
            cancelButtonText: "No, Cancel Plz !",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    $http({
                        method: 'POST',
                        dataType: 'JSON',
                        url: '/Admin/DeleteItemGroup',
                        data: { GroupId: par },
                    }).then(function (res) {
                        if (res.data > 0) {
                            swal("Deleted!", "Your imaginary file has been deleted.", "success");
                            GetAllItemGroup();
                        }
                        else {
                            swal("Not Deleted!", "Some Error Has been Occoured.", "error");
                        }
                    });

                } else {
                    swal("Cancelled", "Your imaginary file is safe :)", "error");
                }
            });
    };
    function resetItemGroup() {
        $scope.GroupId = '';
        $scope.GroupName = '';
        $scope.GroupDesc = '';
        $scope.GroupImage = '';
        angular.element("input[type='file']").val(null);
    }
    function GetAllItemGroup() {
        $("#divSpinner").show();
        $http({
            method: 'POST',
            dataType: 'JSON',
            url: '/Admin/GetAllItemGroup',
            data: {},
        }).then(function (res) {
            if (res.data.length > 0) {
                $scope.dataItemGroups = res.data;
                $("#divSpinner").hide();
            }
        });
    }
});

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
$(function () {
    'use strict';
    $('#datatable1').DataTable({
        responsive: true,
        language: {
            searchPlaceholder: 'Search...',
            sSearch: '',
            lengthMenu: '_MENU_ items/page'
        }
    });
});