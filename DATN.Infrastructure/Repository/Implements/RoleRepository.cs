using DATN.Domain.Entities;
using DATN.Infrastructure.Context;
using DATN.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Infrastructure.Repository.Implements
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(DATNContext context) : base(context)
        {
        }
    }
}
