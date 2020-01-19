using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Tests.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IOrderRepository _orderRepository;



        public OrderHandlerTests()
        {
            _productRepository = new FakeProductRepository();
            _customerRepository = new FakeCustomerRepository();
            _discountRepository = new FakeDiscountRepository();
            _deliveryFeeRepository = new FakeDeliveryFeeRepository();
            _orderRepository = new FakeOrderRepository();

        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmComandoValidoO_PedidoDeveSerGerado()
        {
            var command = new CreateOrderCommand();

            command.Customer = "12345678";
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(
                _customerRepository, 
                _deliveryFeeRepository, 
                _discountRepository,
                _productRepository,
                _orderRepository
            );
            handler.Handle(command);
            Assert.AreEqual(handler.Valid, true);
        }
    }
}
