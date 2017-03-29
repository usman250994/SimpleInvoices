(function () {
    var app = angular.module("ReportModule", ['ngRoute']);
    app.controller('totalSalesController', function ($scope, $http) {

        $scope.message = $http.post('http://localhost:5000/api/reports/totalsales').
            then(function (response) {
                $scope.sales = response.data;
                console.log($scope.sales);
            });

    });

    app.controller("totalSalesCustomers", function ($scope, $http, $rootScope, $location) {

        $scope.message = $http.post('http://localhost:5000/api/reports/totalsalescustomerwise').
            then(function (response) {
                $scope.sales = response.data;
                console.log($scope.sales);
            });
    });
    app.controller("totalSalesBiller", function ($scope, $http, $rootScope, $location) {

        $scope.message = $http.post('http://localhost:5000/api/reports/totalsalesbiller').
            then(function (response) {
                $scope.sales = response.data;
                console.log($scope.sales);
            });
    });
    app.controller("totalTaxes", function ($scope, $http, $rootScope, $location) {

        $scope.message = $http.post('http://localhost:5000/api/reports/totaltaxes').
            then(function (response) {
                $scope.sales = response.data;
                console.log(response.data);
            });
    });

    app.controller("totalProduct", function ($scope, $http, $rootScope, $location) {

        $scope.message = $http.post('http://localhost:5000/api/reports/totalproduct').
            then(function (response) {
                $scope.sales = response.data;
                console.log($scope.sales);
            });

        $scope.getTotal = function () {
            var total = 0;
            for (var i = 0; i < $scope.sales.length; i++) {
                var product = $scope.sales[i].quantity;
                total += product;
            }
            return total;
        };
    });

})();