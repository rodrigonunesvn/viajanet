'use strict';

var myApp = angular.module('myApp', [
    'ngRoute',
    'myApp.home',
    'myApp.landing',
    'myApp.checkout',
    'myApp.confirmacao'
]);

myApp.config(['$locationProvider', '$routeProvider', function ($locationProvider, $routeProvider) {
    $locationProvider.hashPrefix('!');

    $routeProvider.otherwise({ redirectTo: '/home' });
}]);