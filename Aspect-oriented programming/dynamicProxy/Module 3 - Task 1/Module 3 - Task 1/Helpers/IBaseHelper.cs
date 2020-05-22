using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Module3
{
    public interface IBaseHelper
    {
        List<Products> GetCertainAmountOfProduct(NorthwindContext currentContext, int count);
    }
}
