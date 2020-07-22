using System;
using System.Collections.Generic;

namespace EF_Core.Models.ModelView
{
    public class OrderViewModel
    {
        public string CustomerName { get; set; }
        public DateTime? OrderDate { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}