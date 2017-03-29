(function () {
  var app = angular.module("paymentModule", ['ngRoute']);

  app.controller("addpayment", function ($scope, $http) {

    $scope.message = $http.post('http://localhost:5000/api/invoice/getinvoice', 0).
      then(function (response) {
        $scope.invoices = response.data;
        console.log($scope.invoices);

        $scope.save = function () {
          $scope.paymentTypeId = 1;

          $scope.message = $http.post('http://localhost:5000/api/payment/createpayment', $scope.pay).
            then(function (response) {
              $scope.invoices = response.data[0];
            });
        };

      });
  });

  //list of payments
  app.controller("listallpayment", function ($scope, $http) {

    $scope.message = $http.post('http://localhost:5000/api/payment/listpayment', 0).
      then(function (response) {
        $scope.payments = response.data;
        console.log($scope.payments);
      });
  });
  //
})();
