using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mercury.Api.ViewModels;
using Mercury.BLL.Dtos;
using Mercury.BLL.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mercury.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleLogic _scheduleLogic;
        private readonly ILogger _logger;

        public ScheduleController(IScheduleLogic scheduleLogic, ILogger<AuthController> logger)
        {
            _scheduleLogic = scheduleLogic;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetSchedules()
        {
            var response = new ResponseCollection<ScheduleDto>();

            try
            {
                response.Objects = await _scheduleLogic.GetSchedules();
            }
            catch(Exception e)
            {
                response.IsError = true;
                response.ErrorMessage = e.Message;
                _logger.LogError($"ScheduleController | Error | Error message : {e.Message}");
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> GetSchedules([FromBody]ScheduleDto schedule)
        {
            var response = new Response<ScheduleDto>();

            try
            {
                response.Object = await _scheduleLogic.InsertSchedule(schedule);
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.ErrorMessage = "Unexpected error";
                _logger.LogError($"ScheduleController | Error | Error message : {e.Message}");
            }

            return Ok(response);
        }
    }
}