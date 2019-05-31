var myApp = angular.module('myApp.checkout', ['ngRoute']).config(function ($routeProvider) {
    $routeProvider.when('/checkout', {
        templateUrl: 'views/checkout.html',
        controller: 'CheckoutController'
    });
});

myApp.controller('CheckoutController', function ($location, $http, ColetaService, DadosCompraService) {
    this.$onInit = function () {
        ColetaService.coletar("Checkout");
    }; 

    var checkout = this;
    
    checkout.dadosCompra = {};
    checkout.dadosCompra.produtos = DadosCompraService.dadosCompra.produtos;

    checkout.removerProduto = function (index) {
        checkout.dadosCompra.produtos.splice(index, 1);
    };

    checkout.adicionarProdutos = function () {
        DadosCompraService.adicionarProdutos();
        checkout.dadosCompra.produtos = DadosCompraService.dadosCompra.produtos;
    };

    checkout.submeter = function () {
        ColetaService.coletar("Checkout", JSON.stringify(checkout.dadosCompra));

        $location.path("confirmacao");
    };

    checkout.getTotal = function () {
        var total = 0;

        for (var i = 0; i < checkout.dadosCompra.produtos.length; i++) {
            total += checkout.dadosCompra.produtos[i].valor;
        }

        return total;
    };

    DadosCompraService.adicionarProdutos();
});