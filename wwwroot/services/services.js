(function() {

    var app = angular.module('serviceModule', []);

    app.service('getProductId', function() {
        var productId = {};
        return {
            getId: function() {
                return productId;
            },
            setId: function(value) {
                return productId = value;
            }
        };

    });
    app.service('createInvoice', function($http) {
        this.fillscope = function(value) {
            return $http.post("http://localhost:5000/api/product/getproduct", value);
        }
        this.addInvoices = function(invoice) {
            return $http.post("http://localhost:5000/api/invoice/createinvoice", invoice);
        }
    });

    app.service('getTaxService', function($http) {
        this.getTaxes = function() {
            return $http.post("http://localhost:5000/api/tax/gettaxes", 0);
        }
    });
    app.service('getBillerService', function($http) {
        //Get All Employees
        this.getBiller = function(biller) {
            //console.log(name);

            return $http.post("http://localhost:5000/api/invoice/populatedropdown", '"' + biller + '"');
        }

    });
    app.service('getCustomerService', function($http) {


        //Get All Employees
        this.getEmployees = function() {

            return $http.post("http://localhost:5000/api/customer/getCustomers", 0);
        }

    });
})();