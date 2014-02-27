var app = angular.module("silberscarves", []);

app.controller("ProductsCtrl", ["$scope", "$http", "$log", function($scope, $http, $log){
  
    $scope.addToCart = function (id, title, description, price, quantity) {
        var scarfObj = {
            "name": title,
            "price": price,           
            "description": description,
            "scarfId" : id
        };
        $http.post("/Products/AddToCart", scarfObj).success(function (data, status, headers, config) {
            $log.debug(data);
        }).error(function (data, status, headers, config) {
            $log.debug(status)
            $log.debug(data);
        });
    }
    
}]);