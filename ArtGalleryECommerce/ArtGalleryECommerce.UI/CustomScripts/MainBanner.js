var app = angular.module("divMainBannerApp", ['angularUtils.directives.dirPagination']);

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

app.controller("divMainBannerController", function ($scope, $http) {
    GetAllMainBanner();
    GetAllItemMaster();
    resetMainBanner();
    $scope.SaveMainBanner = function () {
        var file = $scope.BannerImage;
        var fd = new FormData();
        fd.append('BannerImage', file);
        fd.append('BannerId', $scope.BannerId);
        fd.append('ItemId', $scope.ItemId);
        fd.append('BannerName', $scope.BannerName);
        fd.append('BannerDesc', $scope.BannerDesc);
        $http({
            method: 'POST',
            dataType: 'JSON',
            url: '/Admin/SaveMainBanner',
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (res) {
            if (res.data > 0) {
                GetAllMainBanner();
                resetMainBanner();
                var element = angular.element('#modalMainBanner');
                element.modal('hide');
                $scope.show = true;
            }
        });
    };
    $scope.EditMainBanner = function (par) {
        resetMainBanner();
        var element = angular.element('#modalMainBanner');
        element.modal('show');
        $scope.BannerId = par.BannerId;
        $scope.ItemId = par.ItemId;
        $scope.BannerName = par.BannerName;
        $scope.BannerDesc = par.BannerDesc;

    };
    $scope.DeleteMainBanner = function (par) {
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
                        url: '/Admin/DeleteMainBanner',
                        data: { BannerId: par },
                    }).then(function (res) {
                        if (res.data > 0) {
                            swal("Deleted!", "Your imaginary file has been deleted.", "success");
                            GetAllMainBanner();
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
        resetMainBanner();
    };
    function resetMainBanner() {
        $scope.BannerId = '';
        $scope.ItemId = '';
        $scope.BannerName = '';
        $scope.BannerDesc = '';
        $scope.BannerImage = '';
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
        });
    }
    function GetAllMainBanner() {
        $http({
            method: 'POST',
            dataType: 'JSON',
            url: '/Admin/GetAllMainBanner',
            data: {},
        }).then(function (res) {
            if (res.data.length > 0) {
                $scope.dataMainBanners = res.data;
            }
        });
    }
});

$(function () {
    $('.modal-effect').on('click', function (e) {
        e.preventDefault();
        var effect = $(this).attr('data-effect');
        $('#modalMainBanner').addClass(effect);
        $('#modalMainBanner').modal('show');
    });
    $('#modalMainBanner').on('hidden.bs.modal', function (e) {
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