var app = angular.module("silberscarves", []);

app.controller("ProductsCtrl", ["$scope", "$http", "$log", function($scope, $http, $log){
  
    $scope.addToCart = function (title, price, quantity) {
        var scarfObj = {
            "title": title,
            "price": price,
            "quantity": quantity
        };
        $http.post("/Products/AddToCart", scarfObj).success(function (data, status, headers, config) {
            $log.debug(data);
        }).error(function (data, status, headers, config) {
            $log.debug(status)
            $log.debug(data);
        });
    }
    
}]);