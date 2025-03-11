using eCommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Domain.Interface.Category
{
    public interface ICategory
    {
        Task<IEnumerable<Product>> GetProductByCategory(Guid categoryId);
    }
}
