using AutoMapper;
using DATN.Application.Dtos.RankQuestionDtos;
using DATN.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankQuestionController : ControllerBase
    {
        private readonly IRankQuestionService _rankQuestionService;
        private readonly IMapper _mapper;
        private readonly ISystemLoggingService _loggingService;
        public RankQuestionController(IRankQuestionService rankQuestionService, IMapper mapper, ISystemLoggingService systemLoggingService)
        {
            _rankQuestionService = rankQuestionService;
            _mapper = mapper;
            _loggingService = systemLoggingService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllRankQuestion()
        {
            var rankQuestions = await _rankQuestionService.GetAllRankAsync();
            var questions = _mapper.Map<IEnumerable<RankQuestionDto>>(rankQuestions);
            return Ok(questions);
        }

    }
}
