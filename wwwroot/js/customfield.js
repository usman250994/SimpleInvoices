(function(){
    var app = angular.module("customModule", ['ngRoute']);
         app.controller("addcustom",function($scope,$http){


$scope.names=["Product","Customer","Biller"];

$scope.save = function(){
    $scope.custom.id = 0;
    $scope.message =  $http.post('http://localhost:5000/api/customfield/addCustomField',$scope.custom).
      then(function (response){
        console.log(response.data);    
 });

}    
  });
app.controller("getcustom",function($scope,$http,$rootScope,$location){
    
    $scope.message =  $http.post('http://localhost:5000/api/customfield/getCustomFieldById',$rootScope.customId).
         then(function (response){        
           $scope.customs = response.data;
               console.log(response.data); 
                });

 $scope.setRoot = function(x){
$rootScope.customId=x;
console.log($rootScope.customId);
        };
 $scope.deleteCustom = function(x){
 $scope.message =  $http.post('http://localhost:5000/api/customfield/deleteCustomFieldById',x).
         then(function (response){        
           $scope.response = response.data;
            
           alert($scope.response.developerMessage);
           $location.path("/customs");
      });
            };
});
        //controller ends here           
     app.controller('editCFController', function($scope,$rootScope,$http){
      alert($rootScope.customId);
$scope.message =  $http.post('http://localhost:5000/api/customfield/getCustomFieldById',$rootScope.customId).
         then(function (response){        
           $scope.customs = response.data[0];
               console.log($scope.customs);  
               $rootScope.customId=0;
                });
                
                $scope.edit = function()
                {          
$scope.message =  $http.post('http://localhost:5000/api/customfield/editCustomField',$scope.customs).
         then(function (response){        
               console.log(response.data);  
                  console.log($scope.customs);  
            });
                }
     });        
    }
                  )
                       ();