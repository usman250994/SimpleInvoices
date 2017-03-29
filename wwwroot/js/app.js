(function(){

    var app = angular.module("dashboardModule", ['ngRoute']);


    app.controller("dashboardCtrl", function($scope,$http,$log,$window) {

      $scope.auth = function(){
$window.location.href = '../shared/side_layout.html';
    //   $scope.message =  $http.post('http://localhost:5000/api/customer/getCustomers','0').
    //     then(function (response) {
    //       console.log(response.data);
    //       $scope.message = response.data;
    //       $log.info(response);
    // });

    }
  }

      );

       }
  )
  ();
