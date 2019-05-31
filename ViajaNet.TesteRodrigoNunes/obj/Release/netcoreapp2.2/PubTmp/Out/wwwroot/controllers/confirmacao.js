var myApp = angular.module('myApp.confirmacao', ['ngRoute']).config(function ($routeProvider) {
    $routeProvider.when('/confirmacao', {
        templateUrl: 'views/confirmacao.html',
        controller: 'ConfirmacaoController'
    });
});

myApp.controller('ConfirmacaoController', function (DadosCompraService, ColetaService) {
    this.$onInit = function () {
        ColetaService.coletar("Confirmação");
    }; 

    var confirmacao = this;
    
    confirmacao.produtos = DadosCompraService.dadosCompra.produtos;

    confirmacao.getTotal = function () {
        var total = 0;

        for (var i = 0; i < confirmacao.produtos.length; i++) {
            total += confirmacao.produtos[i].valor;
        }

        return total;
    };
});


