using AutoMapper;
using DATN.Application.Dtos.RankQuestionDtos;
using DATN.Application.Dtos.RoleDtos;
using DATN.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _roleService;
		private readonly IMapper _mapper;
		private readonly ISystemLoggingService _loggingService;
		public RoleController(IRoleService roleService, IMapper mapper, ISystemLoggingService systemLoggingService)
		{
			_roleService = roleService;
			_mapper = mapper;
			_loggingService = systemLoggingService;
		}

		[HttpGet("getall")]
		public async Task<IActionResult> GetAllRole()
		{
			var roles = await _roleService.GetAllRoleAsync();
			var rolesDto = _mapper.Map<IEnumerable<RoleDto>>(roles);
			return Ok(rolesDto);
		}
	}
}
