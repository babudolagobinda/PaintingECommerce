var app = angular.module("divStockMasterApp", ['angularUtils.directives.dirPagination']);

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

app.controller("divStockMasterController", function ($scope, $http) {
    GetAllStockMaster();
    GetAllItemGroup();
    resetStockMaster();

    $scope.GetCategoryByGroup = function (par) {
        returnCategoryByGroup(par);
    };

    function returnCategoryByGroup(par) {
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
    $scope.GetItemByCategory = function (par) {
        returnItemByCategory(par);
    };

    function returnItemByCategory(par) {
        if (par !== undefined) {
            $http({
                method: 'POST',
                dataType: 'JSON',
                url: '/Admin/GetItemByCategoryId',
                data: { CategoryId: par },
            }).then(function (res) {
                if (res.data.length > 0) {
                    $scope.dataItemMasters = res.data;
                }
            });
        }
        else {
            $scope.CategoryId = {};
        }
    }

    $scope.SaveStockMaster = function () {
        var stockMasterDto = {
            StockId: $scope.StockId,
            GroupId: $scope.GroupId,
            CategoryId: $scope.CategoryId,
            ItemId: $scope.ItemId,
            Quantity: $scope.Quantity
        };
        $http({
            method: 'POST',
            dataType: 'JSON',
            url: '/Admin/SaveStockMaster',
            data: { stockMasterDto: stockMasterDto },
        }).then(function (res) {
            if (res.data > 0) {
                GetAllStockMaster();
                resetStockMaster();
                var element = angular.element('#modalStockMaster');
                element.modal('hide');
                $scope.show = true;
            }
        });
    };
    $scope.EditStockMaster = function (par) {
        resetStockMaster();
        var element = angular.element('#modalStockMaster');
        element.modal('show');
        $scope.StockId = par.StockId;
        $scope.GroupId = par.GroupId;
        returnCategoryByGroup(par.GroupId);
        $scope.CategoryId = par.CategoryId;
        returnItemByCategory(par.CategoryId);
        $scope.ItemId = par.ItemId;
        $scope.Quantity = par.Quantity;

    };
    $scope.DeleteStockMaster = function (par) {
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
                        url: '/Admin/DeleteStockMaster',
                        data: { StockId: par },
                    }).then(function (res) {
                        if (res.data > 0) {
                            swal("Deleted!", "Your imaginary file has been deleted.", "success");
                            GetAllStockMaster();
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
        resetStockMaster();
    };
    function resetStockMaster() {
        $scope.StockId = '';
        $scope.GroupId = '';
        $scope.CategoryId = '';
        $scope.ItemId = '';
        $scope.Quantity = '';
    }
    function GetAllStockMaster() {
        $http({
            method: 'POST',
            dataType: 'JSON',
            url: '/Admin/GetAllStockMaster',
            data: {},
        }).then(function (res) {
            if (res.data.length > 0) {
                $scope.dataStockMasters = res.data;
            }
            else {
                $scope.dataStockMasters = '';
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
        $('#modalStockMaster').addClass(effect);
        $('#modalStockMaster').modal('show');
    });
    $('#modalStockMaster').on('hidden.bs.modal', function (e) {
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