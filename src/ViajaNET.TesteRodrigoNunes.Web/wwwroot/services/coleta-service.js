'use strict';

var myApp = angular.module('myApp');

myApp.service('ColetaService', function ($http) {
    this.coletar = function (pagina, param) {
        var browserName = getBrowserName();
        var dadosColeta = { Pagina: pagina, Browser: browserName, Parametros: param };

        $http.post('/api/coleta/', dadosColeta)
            .then(function successCallback(response) {

            }, function errorCallback(response) {

            });
    };
});

