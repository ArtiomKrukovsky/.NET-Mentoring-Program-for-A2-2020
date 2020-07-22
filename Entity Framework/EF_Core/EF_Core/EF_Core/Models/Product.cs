using System.Collections.Generic;

namespace EF_Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter a value for Product Name")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Line length must be between 4 and 40 characters")]
        public string ProductName { get; set; }

        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a value for Quantity Per Unit")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Line length must be between 4 and 20 characters")]
        public string QuantityPerUnit { get; set; }

        [Required(ErrorMessage = "Please enter a value for Unit Price")]
        [Range(1, 10000)]
        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
