using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Domain.Entities
{
    public class SystemLogging : BaseEntity
    {
        public Guid? UserId { get; set; }
        public string IPAddress { get; set; }
        public string ActionName { get; set; }
        public string Details { get; set; }

        public User? User { get; set; }
    }

}
