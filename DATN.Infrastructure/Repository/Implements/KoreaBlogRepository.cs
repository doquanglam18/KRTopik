using DATN.Domain.Entities;
using DATN.Infrastructure.Context;
using DATN.Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Infrastructure.Repository.Implements
{
    public class KoreaBlogRepository : GenericRepository<KoreaBlog>, IKoreaBlogRepository
    {
        public KoreaBlogRepository(DATNContext context) : base(context)
        {
        }
    }
}
