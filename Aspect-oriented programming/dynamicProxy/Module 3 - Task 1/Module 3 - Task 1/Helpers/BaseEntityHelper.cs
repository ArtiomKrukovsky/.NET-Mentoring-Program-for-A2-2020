namespace Module3
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    public class BaseEntityHelper : IBaseHelper
    {
        public List<Products> GetCertainAmountOfProduct(NorthwindContext currentContext, int count)
        {
            return currentContext.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Take(count).ToList();
        }
    }
}
