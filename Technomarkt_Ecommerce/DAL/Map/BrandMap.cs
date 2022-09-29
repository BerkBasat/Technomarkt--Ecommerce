using Core.Map;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Map
{
    public class BrandMap:CoreMap<Brand>
    {
        public BrandMap()
        {
            ToTable("dbo.Brands");
            Property(x => x.BrandName).HasMaxLength(255).IsRequired();
        }
    }
}
