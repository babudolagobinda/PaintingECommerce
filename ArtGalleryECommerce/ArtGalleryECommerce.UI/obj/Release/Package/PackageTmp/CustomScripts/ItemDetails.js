var app = angular.module("divItemDetailsApp", ['angularUtils.directives.dirPagination']);

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

app.directive('validNumber', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }
            ngModelCtrl.$parsers.push(function (val) {
                if (angular.isUndefined(val)) {
                    val = '';
                }
                var clean = val.replace(/[^-0-9\.]/g, '');
                var negativeCheck = clean.split('-');
                var decimalCheck = clean.split('.');
                if (!angular.isUndefined(negativeCheck[1])) {
                    negativeCheck[1] = negativeCheck[1].slice(0, negativeCheck[1].length);
                    clean = negativeCheck[0] + '-' + negativeCheck[1];
                    if (negativeCheck[0].length > 0) {
                        clean = negativeCheck[0];
                    }
                }
                if (!angular.isUndefined(decimalCheck[1])) {
                    decimalCheck[1] = decimalCheck[1].slice(0, 2);
                    clean = decimalCheck[0] + '.' + decimalCheck[1];
                }

                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });
            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});

app.controller("divItemDetailsController", function ($http, $scope) {
    GetAllItemDetails();
    GetAllItemMaster();
    $scope.SaveItemDetails = function () {
        var itemDetailsDto = {
            ItemDetailsId: $scope.ItemDetailsId,
            ItemId: $scope.ItemId,
            Width: $scope.Width,
            WidthType: $scope.WidthType,
            Height: $scope.Height,
            HeightType: $scope.HeightType,
            Mrp: $scope.Mrp,
            Discount: $scope.Discount,
            Price: $scope.Price
        };
        $http({
            method: 'POST',
            dataType: 'JSON',
            url: '/Admin/SaveItemDetails',
            data: { itemDetailsDto: itemDetailsDto },
        }).then(function (res) {
            if (res.data > 0) {
                GetAllItemDetails();
                resetItemDetails();
                var element = angular.element('#modalItemDetails');
                element.modal('hide');
            }
            else {
                $scope.dataItemMasters = '';
            }
        });
    };
    $scope.EditItemDetails = function (par) {
        resetItemDetails();
        var element = angular.element('#modalItemDetails');
        element.modal('show');
        $scope.ItemDetailsId = par.ItemDetailsId;
        $scope.ItemId = par.ItemId;
        $scope.Width = par.Width;
        $scope.WidthType = par.WidthType;
        $scope.Height = par.Height;
        $scope.HeightType = par.HeightType;
        $scope.Mrp = par.Mrp;
        $scope.Discount = par.Discount;
        $scope.Price = par.Price;

    };
    $scope.DeleteItemDetails = function (par) {
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
                        url: '/Admin/DeleteItemDetails',
                        data: { ItemDetailsId: par },
                    }).then(function (res) {
                        if (res.data > 0) {
                            swal("Deleted!", "Your imaginary file has been deleted.", "success");
                            GetAllItemDetails();
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
        resetItemDetails();
    };
    $scope.CalculatePrice = function () {
        var mrp = parseFloat($scope.Mrp);
        var discount = parseFloat($scope.Discount);
        var discountMrp = mrp * discount / 100;
        var discountPrice = mrp - discountMrp;
        $scope.Price = discountPrice;
    };
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
    function GetAllItemDetails() {
        $http({
            method: 'POST',
            dataType: 'JSON',
            url: '/Admin/GetAllItemDetails',
            data: {},
        }).then(function (res) {
            if (res.data.length > 0) {
                $scope.dataItemDetails = res.data;
            }
            else {
                $scope.dataItemDetails = '';
            }
        });
    }
    function resetItemDetails() {
        $scope.ItemDetailsId = '';
        $scope.ItemId = '';
        $scope.Width = '';
        $scope.WidthType = '';
        $scope.Height = '';
        $scope.HeightType = '';
        $scope.Mrp = '';
        $scope.Discount = '';
        $scope.Price = '';
    }
});

$(function () {
    $('.modal-effect').on('click', function (e) {
        e.preventDefault();
        var effect = $(this).attr('data-effect');
        $('#modalItemDetails').addClass(effect);
        $('#modalItemDetails').modal('show');
    });
    $('#modalItemDetails').on('hidden.bs.modal', function (e) {
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