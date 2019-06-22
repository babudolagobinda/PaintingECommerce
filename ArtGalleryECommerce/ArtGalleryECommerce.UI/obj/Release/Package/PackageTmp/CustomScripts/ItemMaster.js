var app = angular.module("divItemMasterApp", ['angularUtils.directives.dirPagination']);

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

app.controller("divItemMasterController", function ($scope, $http) {
    GetAllItemMaster();
    GetAllItemGroup();
    resetItemMaster();
    $scope.GetCategoryByGroup = function (par) {
        returnGetCategoryByGroup(par);
    };
    function returnGetCategoryByGroup(par) {
        if (par !== undefined) {
            $http({
                method: 'POST',
                dataType: 'JSON',
                url: '/Admin/GetItemCategoryByGroupId',
                data: { GroupId: par },
            }).then(function (res) {
                if (res.data.length > 0) {
                    $scope.dataItemCategories = res.data;
                }
            });
        }
        else {
            $scope.CategoryId = {};
        }
    }
    //$scope.zoomImage = function (filename) {
    //    var file = filename;
    //    var modal = document.getElementById('itemImageModal');
    //    //var img = document.getElementById('ItemImage');
    //    var modalImg = document.getElementById("imgItemImage");
    //    modal.style.display = "block";
    //    modalImg.src = "../UploadImages/" + file;
    //};
    //$scope.CloseItemImage = function () {
    //    var modal = document.getElementById('itemImageModal');
    //    modal.style.display = "none";
    //};
    $scope.SaveItemMaster = function () {
        var file = $scope.ItemImage;
        var fd = new FormData();
        fd.append('ItemImage', file);
        fd.append('GroupId', $scope.GroupId);
        fd.append('CategoryId', $scope.CategoryId);
        fd.append('ItemId', $scope.ItemId);
        fd.append('ItemName', $scope.ItemName);
        fd.append('ItemDesc', $scope.ItemDesc);
        $http({
            method: 'POST',
            dataType: 'JSON',
            url: '/Admin/SaveItemMaster',
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            if (res.data > 0) {
                GetAllItemMaster();
                resetItemMaster();
                var element = angular.element('#modalItemMaster');
                element.modal('hide');
                $scope.show = true;
            }
        });
    };
    $scope.EditItemMaster = function (par) {
        resetItemMaster();
        var element = angular.element('#modalItemMaster');
        element.modal('show');
        $scope.GroupId = par.GroupId;
        returnGetCategoryByGroup(par.GroupId);
        $scope.CategoryId = par.CategoryId;
        $scope.ItemId = par.ItemId;
        $scope.ItemName = par.ItemName;
        $scope.ItemDesc = par.ItemDesc;

    };
    $scope.DeleteItemMaster = function (par) {
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
                        url: '/Admin/DeleteItemMaster',
                        data: { ItemId: par },
                    }).then(function (res) {
                        if (res.data > 0) {
                            swal("Deleted!", "Your imaginary file has been deleted.", "success");
                            GetAllItemMaster();
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
        resetItemMaster();
    };
    function resetItemMaster() {
        $scope.GroupId = '';
        $scope.CategoryId = '';
        $scope.ItemId = '';
        $scope.ItemName = '';
        $scope.ItemDesc = '';
        $scope.ItemImage = '';
        angular.element("input[type='file']").val(null);
    }
    function GetAllItemMaster() {
        $http({
            method: 'POST',
            dataType: 'JSON',
            url: '/Admin/GetAllItemMaster',
            data: {},
        }).then(function (res) {
            if (res.data.length > 0) {
                $scope.dataItemMasters = res.data;
            }
            else {
                $scope.dataItemMasters = '';
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
        $('#modalItemMaster').addClass(effect);
        $('#modalItemMaster').modal('show');
    });
    $('#modalItemMaster').on('hidden.bs.modal', function (e) {
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