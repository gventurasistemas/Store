﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Enums;
using System;

namespace Store.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {

        private readonly Customer _customer = new Customer("André Baltieri", "andre@balta.io");
        private readonly Product _product = new Product("Produto 1", 10, true);
        private readonly Discount _discount = new Discount(10,DateTime.Now.AddDays(5));

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmNovoPedidoValidoEleDeveGerarUmNumeroCom8Caracteres()
        {
            var order = new Order(_customer, 0, null);
            Assert.AreEqual(8, order.Number.Length);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmNovoPedidoSeuStatusDeveSerAguardandoPagamento()
        {           
            var order = new Order(_customer, 0, null);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmPagamentoDoPedidoSeuStatusDeveSerAguardandoEntrega()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product,1);
            order.Pay(10);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
        }
        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmPedidoCanceladoSeuStatusDeveSerCancelado()
        {
            var order = new Order(_customer, 0, null);
            order.Cancel();
            Assert.AreEqual(order.Status, EOrderStatus.Canceled);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmNovoItemSemProdutoOhMesmoNaoDeveSerAdicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(null,10);
            Assert.AreEqual(order.Items.Count, 0);
        }
        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmNovoItemComQuantidadeZeroOuMenorOhMesmoNaoDeveSerAdicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 0);
            Assert.AreEqual(order.Items.Count, 0);
        }
        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmNovoPedidoValidoSeuTotalDeveSer50()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);           
            Assert.AreEqual(order.Total(), 50);
        }
        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmDescontoExpiradoOValorDoPedidoDeveSer60()
        {
            var expiredDiscount = new Discount(10,DateTime.Now.AddDays(-5));//Desconto Expirado
            var order = new Order(_customer, 10, expiredDiscount);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 60);
        }
        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmDescontoInvalidoOValorDoPedidoDeveSer60()
        {            
            var order = new Order(_customer, 10, null);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 60);
        }
        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmDescontoDeDezOValorDoPedidoDeveSer50()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 50);
        }
        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmaTaxaDeEntregaDe10OValorDoPedidoDeveSer60()
        {           
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 6);
            Assert.AreEqual(order.Total(), 60);
        }
        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmPedidoSemClienteOhMesmoDeveSerInvalido()
        {
            var order = new Order(null, 10, _discount);            
            Assert.AreEqual(order.Valid, false);
        }

    }
}
