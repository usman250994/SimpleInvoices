(function(){
    var app = angular.module("billerModule", ['ngRoute']);
         app.controller("addBiller",function($scope,$http,$log,customfields){
$scope.customFields = [];
var getResult =  customfields.get('"Biller"');
getResult.then(function(pl) { 
    $scope.customFields = pl.data 
for(var i=0;i<$scope.customFields.length;i++)
{
    $scope.customFields[i].fieldValue="";
}
},
                function(errorPl) {
                    $log.error('failure loading customeFields', errorPl);
                });

$scope.counter = 0;
$scope.save= function(){
$scope.biller.id=0;
$scope.biller.customFields=$scope.customFields;
console.log($scope.biller);
    $scope.message =  $http.post('http://localhost:5000/api/biller/create',$scope.biller).
      then(function (response){
        
 });
}    
  });

app.controller("getBiller",function($scope,$http,$rootScope,$location){
    
    $scope.message =  $http.post('http://localhost:5000/api/biller/getBiller',$rootScope.billerId).
         then(function (response){        
           $scope.billers = response.data;
               
                });

 $scope.setRoot = function(x){
$rootScope.billerId=x;
        };

 $scope.deleteBiller = function(x){
 $scope.message =  $http.post('http://localhost:5000/api/biller/delete',x).
         then(function (response){        
           $scope.response = response.data;
           
           alert($scope.response.developerMessage);
          
           $location.path("/billers");
                });
        };

});           
     app.controller('editBController', function($scope,$rootScope,$http){
$scope.message =  $http.post('http://localhost:5000/api/biller/getBiller',$rootScope.billerId).
         then(function (response){        
           $scope.billers = response.data[0];
               $rootScope.billerId=0;
                });

                $scope.edit = function()
                {         
                    
 $scope.message =  $http.post('http://localhost:5000/api/biller/edit',$scope.billers).
         then(function (response){        
               
                
            });
                }
     });        
        
    }
                  )
                       ();