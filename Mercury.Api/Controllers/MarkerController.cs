using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mercury.Api.ViewModels;
using Mercury.BLL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mercury.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MarkerController : ControllerBase
    {
        private readonly IMarkerLogiccs _markerService;
        private readonly ILogger _logger;

        public MarkerController(IMarkerLogiccs markerService, ILogger<MarkerController> logger)
        {
            _markerService = markerService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMarker(long id)
        {
            var response = new Response<MarkerDto>();

            try
            {
                response.Object = await _markerService.GetMarker(id);
            }
            catch(InvalidOperationException e)
            {
                response.IsError = true;
                response.ErrorMessage = "There is no marker with given identification";

                _logger.LogError($"MarkerController | Error | Error message : {e.Message}");
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetMarkers()
        {
            var response = new ResponseCollection<MarkerDto>();

            try
            {
                response.Objects = await _markerService.GetMarkers();
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.ErrorMessage = e.Message;

                _logger.LogError($"MarkerController | Error | Error message : {e.Message}");
            }

            return Ok(response);
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateMarker([FromBody]MarkerDto marker)
        {
            var response = new Response<MarkerDto>();

            try
            {
                response.Object = await _markerService.CreateMarker(marker);
            }
            catch (InvalidOperationException e)
            {
                response.IsError = true;
                response.ErrorMessage = "Data provided by client is not valid";

                _logger.LogError($"MarkerController | Error | Error message : {e.Message}");
            }

            return Ok(response);
        }

        //[Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteMarker([FromQuery]long markerId)
        {
            var response = new Response<MarkerDto>();

            _logger.LogError($"MarkerController | INFO | Deleting : {markerId}");

            try
            {
                await _markerService.DeleteMarker(markerId);
            }
            catch (InvalidOperationException e)
            {
                response.IsError = true;
                response.ErrorMessage = "There is no marker with given identification";

                _logger.LogError($"MarkerController | Error | Error message : {e.Message}");
            }

            return Ok(response);
        }

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateMarker([FromBody]MarkerDto marker)
        {
            var response = new Response<MarkerDto>();

            _logger.LogError($"MarkerController | INFO | Updating : {marker.Id}");

            try
            {
                response.Object = await _markerService.UpdateMarker(marker);
            }
            catch (InvalidOperationException e)
            {
                response.IsError = true;
                response.ErrorMessage = "There is no marker with given identification";

                _logger.LogError($"MarkerController | Error | Error message : {e.Message}");
            }

            return Ok(response);
        }
    }
}