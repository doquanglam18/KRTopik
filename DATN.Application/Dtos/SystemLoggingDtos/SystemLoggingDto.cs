using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.SystemLoggingDtos
{
    public class SystemLoggingDto
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public string IPAddress { get; set; }
        public string ActionName { get; set; }
        public string Details { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UserName { get; set; }
    }
}
