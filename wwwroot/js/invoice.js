(function () {
    var app = angular.module("invoiceModule", ['ngRoute']);
    app.controller('createInvoiceController', function ($scope, getBillerService, getTaxService, $rootScope, $log, createInvoice, getProductId) {
        $scope.invoices = [];
        $scope.invoice = [];

        var d1 = {
            color: '',
            cut: '',
            fabric: '',
            note: '',
            name: 'dupatta'
        };
        var d2 = {
            color: '',
            cut: '',
            fabric: '',
            note: '',
            name: 'pant'
        };
        var d3 = {
            color: '',
            cut: '',
            fabric: '',
            note: '',
            name: 'kurta'
        };
        var product = {
            id: 0,
            name: '',
            price: '',
            quantity: '',
            taxId: '',
            designs: [],
            color: '',
            note: '',
            description: ''
        };
        product.designs.push(d1);
        product.designs.push(d2);
        product.designs.push(d3);
        $scope.invoices.push(product);
        $scope.addProduct = function () {
            var d1 = {
                color: '',
                cut: '',
                fabric: '',
                note: '',
                name: 'dupatta'
            };
            var d2 = {
                color: '',
                cut: '',
                fabric: '',
                note: '',
                name: 'pant'
            };
            var d3 = {
                color: '',
                cut: '',
                fabric: '',
                note: '',
                name: 'kurta'
            };
            var product = {
                id: 0,
                name: '',
                price: '',
                quantity: '',
                taxId: '',
                designs: [],
                color: '',
                note: '',
                description: ''
            };
            product.designs.push(d1);
            product.designs.push(d2);
            product.designs.push(d3);
            $scope.invoices.push(product);
            console.log($scope.invoices);
        };

        $scope.removeProduct = function () {
            $scope.invoices.splice(-1, 1);
            console.log($scope.invoices);
        };

        $scope.updateProduct = function (index) {
            for (var i = 0; i < $scope.products.length; i++) {
                if ($scope.products[i].name == $scope.invoices[index].name) {
                    $scope.invoices[index].price = $scope.products[i].price;
                    $scope.invoices[index].id = $scope.products[i].id;
                    return;
                }
            }
        }
        $scope.updateTax = function (id, index) {
            $scope.invoices[index].taxId = id;
        };
        $scope.save = function () {
            var addInvoice = { products: $scope.invoices, note: $scope.note, date: $scope.date, biller: $scope.biller, customer: $scope.customer }
     var promiseGet = createInvoice.addInvoices(addInvoice);
        promiseGet.then(function (pl) {
            $scope.result = pl.data
           $location.url('invoices');
        },
            function (errorpl) {
                $log.error('Failure loading Tax', errorpl);
                console.log($scope.result);
            });  
      };
       



        $scope.cancel = function () {
            $scope.invoices = [];
        };
        var product = "Product";
        var promiseGet = createInvoice.fillscope(0); //The MEthod Call from service

        promiseGet.then(function (pl) {
            $scope.products = pl.data
        },
            function (errorPl) {
                $log.error('failure loading products', errorPl);
            });

        var promiseGet = getTaxService.getTaxes();
        promiseGet.then(function (pl) {
            $scope.taxes = pl.data
        },
            function (errorpl) {
                $log.error('Failure loading Tax', errorpl);
            });

        var biller = "biller";
        var promiseGet = getBillerService.getBiller(biller); //The MEthod Call from service
        promiseGet.then(function (pl) {
            $scope.billers = pl.data
        },
            function (errorPl) {
                $log.error('failure loading Biller', errorPl);
            });
        var customer = "Customer";
        var promiseGet = getBillerService.getBiller(customer); //The MEthod Call from service

        promiseGet.then(function (pl) {
            $scope.customers = pl.data
        },
            function (errorPl) {
                $log.error('failure loading Customers', errorPl);
            });
    });


    app.controller('viewinvoices', function ($scope, $rootScope, $http,$location) {

        $scope.message = $http.post('http://localhost:5000/api/invoice/getinvoice', $rootScope.invoiceId).
            then(function (response) {
                $scope.invoices = response.data;
                //$scope.customs = $scope.Customers[0].customfields;
                console.log(response.data);
                $rootScope.invoiceId = 0;
            });

        $scope.setRoot = function (x) {
            $rootScope.invoiceId = x;
            console.log($rootScope.invoiceId);
        };


$scope.checkedit = function(x)
{
$scope.message =  $http.post('http://localhost:5000/api/invoice/getinvoice',x).
         then(function (response){        

           $scope.customers = response.data[0];   
               console.log(response.data[0]);  
                
                if($scope.customers.balance==0)
                {
               $location.path("/invoices");
                }  
                else
                {
   $rootScope.invoiceId = x;
     $location.path("/editinvoices");
                }
                });

}
        $scope.deleteInvoice = function (x) {
            $scope.message = $http.post('http://localhost:5000/api/invoice/deleteinvoice', x).
                then(function (response) {
                    $scope.response = response.data;
                    console.log(response.data);
                    alert($scope.response.developerMessage);
                    $location.path("/invoices");
                });
        };



    });
//
     app.controller('editIController', function($scope,$rootScope,$http){
        
   $scope.message =  $http.post('http://localhost:5000/api/invoice/getinvoice',$rootScope.invoiceId).
         then(function (response){        

           $scope.customers = response.data[0];   
           $scope.customers.date=$scope.customers.date.substring(0,10);
           $scope.customers.delivery=$scope.customers.delivery.substring(0,10);
               console.log(response.data[0]);  
                 $rootScope.invoiceId=0;
       

            
         });

//print function here
$scope.print= function()
{
 var printContents = document.getElementById(viewInvoices).innerHTML;
  var popupWin = window.open('', '_blank', 'width=150,height=150');
  popupWin.document.open();
  popupWin.document.write('<html><head>  <link rel="stylesheet" href="css/bootstrap.min.css"><link rel="stylesheet" href="css/font-awesome/css/font-awesome.min.css"><link rel="stylesheet" href="views/css/stylesheet.css"></head><body onload="window.print()">' + printContents + '</body></html>');
  popupWin.document.close();   
}
$scope.exports = function(){
console.log("sdsd");
html2canvas(document.getElementById('exportthis'), {
            onrendered: function (canvas) {
                var data = canvas.toDataURL();
                var docDefinition = {
                    content: [{
                        image: data,
                        width: 500,
                    }]
                };
                pdfMake.createPdf(docDefinition).download("Score_Details.pdf");
            }
        });
     }

                $scope.edit = function()
                {                   
var file = document.getElementById('myfile').files[0];
  if(file){var reader = new FileReader();
     reader.readAsBinaryString(file);
 reader.onload = function(e) {
$scope.Customers.imagepath= btoa(reader.result);
console.log($scope.Customers);
$scope.message =  $http.post('http://localhost:5000/api/customer/edit',$scope.Customers).
         then(function (response){        
               console.log(response.data);  
                });
  } }




                }

     });
//  edit invoice page directions are set here
app.controller('editInvoice', function($scope,$rootScope,$http,getBillerService, getTaxService,createInvoice, getProductId){

   $scope.message =  $http.post('http://localhost:5000/api/invoice/getinvoice',$rootScope.invoiceId).
         then(function (response){        

          $scope.customers = response.data[0];   
                 $rootScope.invoiceId=0; 
                 $scope.bille = $scope.customers.billerName;
                 $scope.custome = $scope.customers.custName;
                 $scope.tax= $scope.customers.taxPercent;
                 $scope.quantity= $scope.customers.quantity;
                 $scope.created = $scope.customers.date.substring(0,10);
                 $scope.date = $scope.customers.delivery.substring(0,10);
          
         for(var i =0;i<$scope.customers.product.length;i++)
         {
             $scope.customers.product[i].total = $scope.customers.product[i].quantity*$scope.customers.product[i].price*$scope.customers.product[i].taxPercent*0.01+$scope.customers.product[i].quantity*$scope.customers.product[i].price;
    alert($scope.customers.product[i].total);    
     }
var old = [];
          for(var i=0;i<$scope.customers.product.length;i++)
         {
             old[i]=$scope.customers.product[i].price;
     }
        
          for(var i=0;i<$scope.customers.product.length;i++)
         {
             $scope.$watch('customers.product['+i+'].total',function(newValue,oldValue)
             {
                 var old =$scope.customers.price; 
$scope.customers.price = $scope.customers.price+newValue; 
$scope.customers.price = $scope.customers.price-oldValue;
    var diff = $scope.customers.price -old;

    $scope.customers.balance = $scope.customers.balance+diff;
         });
     }

         });
$scope.edits = function()
{
    console.log("ssss");
$scope.customers.billerId=$scope.bill;
$scope.customers.customerId=custom;
$scope.customers.delivery=dates;
console.log($scope.customers);
//here take your $scope.customers invoice  variable :) have fun
}      

//product
var product = "Product";
        var promiseGet = createInvoice.fillscope(0); //The MEthod Call from service
        promiseGet.then(function (pl) {
            $scope.products = pl.data
             },
            function (errorPl) {
                $log.error('failure loading products', errorPl);
            });
//taxes
        var promiseGet = getTaxService.getTaxes();
        promiseGet.then(function (pl) {
            $scope.taxes = pl.data
        },
            function (errorpl) {
                $log.error('Failure loading Tax', errorpl);
            });
//billers
        var biller = "biller";
        var promiseGet = getBillerService.getBiller(biller); //The MEthod Call from service
        promiseGet.then(function (pl) {
            $scope.billers = pl.data
        },
            function (errorPl) {
                $log.error('failure loading Biller', errorPl);
            });
       
    //    customers
        var customer = "Customer";
        var promiseGet = getBillerService.getBiller(customer); //The MEthod Call from service

        promiseGet.then(function (pl) {
            $scope.customer = pl.data
        },
            function (errorPl) {
                $log.error('failure loading Customers', errorPl);
            });     
            

$scope.updatePrice= function(index)
{
    var value = document.getElementById('prod-'+index).value;
    var prod = $scope.customers.product[index];
for(var i=0;i<$scope.products.length;i++)
 { 
    if($scope.products[i].name==value)
   {         prod.name= $scope.products[i].name;
       prod.price = $scope.products[i].price;
       //call function to update price and all other fields wrt product change
    $scope.customers.product[index] = prod;
    prod.total = prod.quantity*prod.price*prod.taxPercent*0.01+prod.quantity*prod.price;

    return;
    }
   }
}
//blur function editProd
$scope.editProd = function(index,name)
{
if(name=="quantity")
{ 
    alert("Quantity");
    // now that quantity hass changed so we need to change price accordingly
    var prod = $scope.customers.product[index]; 
  prod.total = prod.quantity*prod.price*prod.taxPercent*0.01+prod.quantity*prod.price;
$scope.customers.product[index]=prod;
}
if(name=="tax")
{
    
    alert("eeach");// now that tax hass changed so we need to change price accordingly
var prod = $scope.customers.product[index]; 
var taxId = document.getElementById('tax-'+index).value;
for(var i=0;i<$scope.taxes.length;i++)
{
if($scope.taxes[i].id==taxId)
{
prod.taxPercent= $scope.taxes[i].percent;
}
    } 
    alert("reached");
  prod.total = prod.quantity*prod.price*prod.taxPercent*0.01+prod.quantity*prod.price;
$scope.customers.product[index]=prod;
}
if(name=="price")
{
       // now that price hass changed so we need to change price accordingly
    var prod = $scope.customers.product[index]; 
    alert(prod.taxPercent);
    alert(prod.price);
    alert(prod.quantity);
 
  prod.total = prod.quantity*prod.price*prod.taxPercent*0.01+prod.quantity*prod.price;
$scope.customers.product[index]=prod;
}

    }



     });
//
})
    ();