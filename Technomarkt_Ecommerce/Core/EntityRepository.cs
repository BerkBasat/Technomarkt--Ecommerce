using Core.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Core
{
    public class EntityRepository : IEntity<Guid>
    {

        public EntityRepository()
        {
            Status = Status.Active;
            CreatedDate = DateTime.Now.ToString("dddd, dd MMMM yyyy", new CultureInfo("en-GB")); //Change datetime format to English
            CreatedComputerName = Environment.MachineName;
            CreatedIP = RemoteIpAddress.GetIpAddress();
            CreatedAdUsername = "Admin";
            CreatedBy = "Admin";
        }

        public Guid ID { get; set; }
        public Status Status { get; set; }


        public string CreatedDate { get; set; }
        public string CreatedComputerName { get; set; }
        public string CreatedIP { get; set; }
        public string CreatedAdUsername { get; set; }
        public string CreatedBy { get; set; }


        public string UpdatedDate { get; set; }
        public string UpdatedComputerName { get; set; }
        public string UpdatedIP { get; set; }
        public string UpdatedAdUsername { get; set; }
        public string UpdatedBy { get; set; }


    }
}
