var myApp = angular.module('myApp.landing', ['ngRoute']).config(function ($routeProvider) {
    $routeProvider.when('/landing', {
        templateUrl: 'views/landing.html',
        controller: 'LandingController'
    });
});

myApp.controller('LandingController', function ($location, ColetaService) {
    this.$onInit = function () {
        ColetaService.coletar("Landing page");
    };    

    var landing = this;
    landing.dadosContato = {};

    landing.enviar = function () {        
        ColetaService.coletar("Landing page", JSON.stringify(landing.dadosContato));

        $location.path("checkout");
    };
});