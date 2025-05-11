using CloudinaryDotNet.Actions;
using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using DATN.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Implements
{
	public class RoleService : IRoleService
	{
		protected readonly IUnitOfWork _unitOfWork;
		public RoleService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<Domain.Entities.Role>> GetAllRoleAsync()
		{
			var roles = await _unitOfWork.RoleRepository.GetAll().ToListAsync();
			return roles;
		}
	}

}
