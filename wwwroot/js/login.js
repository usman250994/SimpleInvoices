(function () {
    var app = angular.module("loginModule", ['ngRoute']);
    app.controller('loginController', function ($scope, $http, $log) {
        $scope.login = { id: 0, email: '', password: '' };
        $scope.validate = function () {
            console.log($scope.login);
            $scope.message = $http.post('http://localhost:5000/api/login/biller', $scope.login).
                then(function (response) {
                    $scope.result = response.data
                });
            console.log($scope.result);
        };

    });

})();