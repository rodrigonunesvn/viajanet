var myApp = angular.module('myApp');

myApp.service('DadosCompraService', function () {
    this.dadosCompra = [];

    this.adicionarProdutos = function () {
        this.dadosCompra.produtos = [
            { nome: 'iPhone 8 Cinza Espacial ', descricao: '4,7 , 4g, 64gb, 12 Mp - Mq6g2br/a', valor: 3819.50 },
            { nome: 'Apple Watch Serie 3', descricao: '42mm Pulseira Preta Gps', valor: 1659 },
            { nome: 'iPad New', descricao: 'Wifi Modelo 2018 Todas as Cores', valor: 1819 },
            { nome: 'Macbook Pro Touchbar', descricao: '15 I7 2.6 32gb 1tb', valor: 18999 }];
    };
});
