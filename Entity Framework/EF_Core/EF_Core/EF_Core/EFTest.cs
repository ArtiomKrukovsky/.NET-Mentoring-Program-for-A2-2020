using System.Collections.Generic;
using System.Linq;
using EF_Core.Models;
using EF_Core.Models.ModelView;
using Xunit;
using Xunit.Abstractions;

namespace EF_Core
{
    public class EFTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly NorthwindDbContext _northwindDbContext;

        public EFTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _northwindDbContext = new NorthwindDbContext();
        }

        [Fact]
        public void TaskOne_SelectData()
        {
            var categoryName = "Phones";

            var orders = (from order in _northwindDbContext.Orders
                from oDetail in order.OrderDetails
                where oDetail.Product.Category.CategoryName == categoryName
                select new OrderViewModel()
                {
                    CustomerName = order.Customer.CompanyName,
                    Products = (List<ProductViewModel>)
                        (from orderDetail in order.OrderDetails
                        select new ProductViewModel
                        {
                            ProductName = orderDetail.Product.ProductName,
                            UnitPrice = orderDetail.UnitPrice,
                            Quantity = orderDetail.Quantity
                        }),
                    OrderDate = order.OrderDate
                }).Distinct();

            foreach (var order in orders)
            {
                WriteOrdersValue(order);
            }
        }

        private void WriteOrdersValue(OrderViewModel order)
        {
            _testOutputHelper.WriteLine($"Customer name: {order.CustomerName} | Order date: {order.OrderDate}");
            WriteProductValues(order.Products);
        }

        private void WriteProductValues(IEnumerable<ProductViewModel> products)
        {
            _testOutputHelper.WriteLine("Products:");
            foreach (var product in products)
            {
                _testOutputHelper.WriteLine($"- Name: {product.ProductName} | Quantity: {product.Quantity} | Price: {product.UnitPrice}");
            }
        }
    }
}
