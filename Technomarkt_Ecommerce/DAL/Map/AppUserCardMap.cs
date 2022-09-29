using Core.Map;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Map
{
    public class AppUserCardMap:CoreMap<AppUserCard>
    {
        public AppUserCardMap()
        {
            ToTable("dbo.AppUserCards");
            Property(x => x.CardHolderName).HasMaxLength(255).IsRequired();
            Property(x => x.CardNumber).HasMaxLength(255).IsRequired();
            Property(x => x.CardType).HasMaxLength(255).IsRequired();
            Property(x => x.ExpirationDate).HasMaxLength(255).IsRequired();
            Property(x => x.CVC).HasMaxLength(255).IsRequired();
        }
    }
}
