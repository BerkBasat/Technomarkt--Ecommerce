using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class UserAndComment
    {
        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public Guid UserCommentId { get; set; }
        public virtual UserComment UserComment { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
