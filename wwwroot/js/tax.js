( function(){
    var app = angular.module("taxModule", ['ngRoute']);
    app.controller('taxController',function($scope,$http){
        
       $scope.save = function(){
    $scope.tax.id = 0;
    $scope.message =  $http.post('http://localhost:5000/api/tax/create',$scope.tax).
      then(function (response){
        console.log(response.data);    
 });

}
       
    });

app.controller("gettax",function($scope,$http,$rootScope,$location){
    
    $scope.message =  $http.post('http://localhost:5000/api/tax/gettaxes',$rootScope.taxId).
         then(function (response){        
           $scope.taxes = response.data;
               console.log(response.data); 
                });

 $scope.setRoot = function(x){
$rootScope.taxId=x;
console.log($rootScope.taxId);
        };
 $scope.deletetax = function(x){
 $scope.message =  $http.post('http://localhost:5000/api/tax/deletetaxes',x).
         then(function (response){        
           $scope.response = response.data;
            
           alert($scope.response.developerMessage);
           $location.path("/tax");
      });
            };
});



   app.controller('editTController', function($scope,$rootScope,$http){

$scope.message =  $http.post('http://localhost:5000/api/tax/gettaxes',$rootScope.taxId).
         then(function (response){        
           $scope.taxes = response.data[0];
               console.log($scope.taxes);  
               $rootScope.taxId=0;
                });
                $scope.edit = function()
                {          
   console.log($scope.taxes);
$scope.message =  $http.post('http://localhost:5000/api/tax/edittaxes',$scope.taxes).
         then(function (response){        
               console.log(response.data);  
                  console.log($scope.taxes);  
            });
                }
     }); 


























})();