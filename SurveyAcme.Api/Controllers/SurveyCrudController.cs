using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyAcme.Api.Business;
using SurveyAcme.Models.Inputs;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SurveyAcme.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyCrudController : ControllerBase
    {
        private readonly SurveyCrudService _service;
        private readonly ILogger<SurveyCrudController> _logger;

        public SurveyCrudController(ILogger<SurveyCrudController> logger, SurveyCrudService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSurvery(SurveyCreateIn request)
        {
            var response = await _service.RegisterSurvey(request);
            request.Id = response.ID;
            request.Link = response.Message;
            await _service.UpdateSurvey(request);
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListSurvery()
        {
            var response = await _service.ListSurvey();
            return Ok(response);
        }

        [HttpPost("InsertSurvey")]
        public async Task<IActionResult> InsertSurvey([FromQuery] SurveyCrudInsertRequest request)
        {
            int surveyId = request.SurveyId;
            List<SurveyFieldInsert> fields = request.Fields;

            var response = await _service.InsertSurvey(fields);

            return Ok(response);
        }

        [HttpGet("GenerateToken")]
        public IActionResult GenerateToken()
        {
            var response = _service.GenerateJwtToken();
            return Ok(response);
        }

    }
}
