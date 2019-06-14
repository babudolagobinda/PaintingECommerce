var app = angular.module("divItemCategoryApp", ['angularUtils.directives.dirPagination']);

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

app.controller("divItemCategoryController", function ($scope, $http) {
    GetAllItemGroup();
    GetAllItemCategory();
    resetItemCategory();
    $scope.SaveItemCategory = function () {
        var file = $scope.CategoryImage;
        var fd = new FormData();
        fd.append('CategoryImage', file);
        fd.append('GroupId', $scope.GroupId);
        fd.append('CategoryId', $scope.CategoryId);
        fd.append('CategoryName', $scope.CategoryName);
        fd.append('CategoryDesc', $scope.CategoryDesc);
        $http({
            method: 'POST',
            dataType: 'JSON',
            url: '/Admin/SaveItemCategory',
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            if (res.data > 0) {
                GetAllItemCategory();
                resetItemCategory();
                var element = angular.element('#modalItemCategory');
                element.modal('hide');
                $scope.show = true;
            }
        });
    };
    $scope.EditItemCategory = function (par) {
        resetItemCategory();
        var element = angular.element('#modalItemCategory');
        element.modal('show');
        $scope.GroupId = par.GroupId;
        $scope.CategoryId = par.CategoryId;
        $scope.CategoryName = par.CategoryName;
        $scope.CategoryDesc = par.CategoryDesc;

    };
    $scope.DeleteItemCategory = function (par) {
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
                        url: '/Admin/DeleteItemCategory',
                        data: { CategoryId: par },
                    }).then(function (res) {
                        if (res.data > 0) {
                            swal("Deleted!", "Your imaginary file has been deleted.", "success");
                            GetAllItemCategory();
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
    $scope.resetAll = function () {
        resetItemCategory();
    };
    function resetItemCategory() {
        $scope.GroupId = '';
        $scope.CategoryId = '';
        $scope.CategoryName = '';
        $scope.CategoryDesc = '';
        $scope.CategoryImage = '';
        angular.element("input[type='file']").val(null);
    }
    function GetAllItemCategory() {
        $http({
            method: 'POST',
            dataType: 'JSON',
            url: '/Admin/GetAllItemCategory',
            data: {},
        }).then(function (res) {
            if (res.data.length > 0) {
                $scope.dataItemCategories = res.data;
            }
        });
    }
    function GetAllItemGroup() {
        $http({
            method: 'POST',
            dataType: 'JSON',
            url: '/Admin/GetAllItemGroup',
            data: {},
        }).then(function (res) {
            if (res.data.length > 0) {
                $scope.dataItemGroups = res.data;
            }
        });
    }
});

$(function () {
    $('.modal-effect').on('click', function (e) {
        e.preventDefault();
        var effect = $(this).attr('data-effect');
        $('#modalItemCategory').addClass(effect);
        $('#modalItemCategory').modal('show');
    });
    $('#modalItemCategory').on('hidden.bs.modal', function (e) {
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