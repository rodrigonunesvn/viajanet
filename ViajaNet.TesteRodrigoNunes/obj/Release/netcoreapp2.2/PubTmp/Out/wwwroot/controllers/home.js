var myApp = angular.module('myApp.home', ['ngRoute']).config(function ($routeProvider) {
    $routeProvider.when('/home', {
        templateUrl: 'views/home.html',
        controller: 'HomeController'
    });
});

myApp.controller('HomeController', function (ColetaService) {    
    this.$onInit = function () {
        ColetaService.coletar("Home");
    };    
});
