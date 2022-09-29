using DAL.Entity;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class ProductService : BaseService<Product>
    {

        public string Activate(Guid id)
        {
            try
            {
                Product product = GetById(id);
                product.Status = Core.Enums.Status.Active;
                Update(product);
                return "Item is now available in the store!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Remove(Guid id)
        {
            try
            {
                Product product = GetById(id);
                product.Status = Core.Enums.Status.Inactive;
                Update(product);
                return "Item is currently unavailable in the store!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Discontinue(Guid id)
        {
            try
            {
                Product product = GetById(id);
                product.Status = Core.Enums.Status.Discontinued;
                Update(product);
                return "Item is discontinued!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
