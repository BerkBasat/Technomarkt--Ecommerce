using Core.Map;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Map
{
    public class UserCommentMap:CoreMap<UserComment>
    {
        public UserCommentMap()
        {
            ToTable("dbo.UserComments");
            Property(x => x.Author).IsRequired().HasMaxLength(255);
            Property(x => x.Comment).IsRequired();
        }
    }
}
