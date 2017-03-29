(
    function(){
  var app = angular.module("customservice", []);

app.service('customfields',function($http){
this.get = function(x) {
      return   $http.post('http://localhost:5000/api/customfield/getcustomfield',x);
      
     }

});
    }
  )
  ();
