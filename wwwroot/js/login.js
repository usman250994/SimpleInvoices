(function () {
    var app = angular.module("loginModule", []);
    app.controller('loginController', function ($scope, $http, $log) {
        $scope.login = { id: 0, email: '', password: '' };
        $scope.validate = function () {
            
            $scope.message = $http.post('http://localhost:5000/api/login/biller', $scope.login).
                then(function (response) {
                   if(response.data.status!=1)
            {

    window.location = "index2.html";
           
         }
            else
            {
                alert("Hello You are logging Incoorectly");
            }
                });
            //console.log($scope.result);
           
        };

    });

})();