using System;
using System.Collections.Generic;

namespace Module3
{
    using System.ComponentModel.DataAnnotations;

    public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a value for Category Name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Line length must be between 3 and 15 characters")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Please enter a value for Description")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Line length must be between 3 and 500 characters")]
        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
