(function(){
    var app = angular.module("productModule", ['ngRoute','customservice']);


         app.controller("manageDesign",function($scope,$http,$log,customfields){
$scope.customFields = [];
var getResult =  customfields.get('"Product"');
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
$scope.save= function(){
$scope.product.id=0;
 $scope.product.customField=$scope.customFields;
console.log($scope.product);
    $scope.message =  $http.post('http://localhost:5000/api/product/addproduct',$scope.product).
         then(function (response){
           console.log(response.data);    
           $scope.message = response.data;
          
 });

 
}    
  });

app.controller("getProduct",function($scope,$http,$log,$rootScope,$location){

    $scope.message =  $http.post('http://localhost:5000/api/product/getproduct',$rootScope.ProductId).
         then(function (response){        
           $scope.Products = response.data;
           
               console.log(response.data);  
                });
 $scope.setRoot = function(x){
$rootScope.ProductId=x;
console.log($rootScope.ProductId);
        };
         $scope.deleteProduct = function(x){
 $scope.message =  $http.post('http://localhost:5000/api/product/deleteproduct',x).
         then(function (response){        
           $scope.response = response.data;
            console.log(response.data);  
           alert($scope.response.developerMessage);
           //  route to customer.html
           $location.path( "/manageproduct" );
                });
        };

});

        //controller ends here
              
     app.controller('editPController', function($scope,$rootScope,$http){
$scope.message =  $http.post('http://localhost:5000/api/product/getproduct',$rootScope.ProductId).
         then(function (response){        
           $scope.products = response.data[0];
               console.log(response.data[0]);  
               $rootScope.ProductId=0;
                });

                $scope.edit = function()
                {          
$scope.message =  $http.post('http://localhost:5000/api/product/editproduct',$scope.products).
         then(function (response){        
               console.log(response.data);  
                });
                }

     });        
        
    }
                  )
                       ();