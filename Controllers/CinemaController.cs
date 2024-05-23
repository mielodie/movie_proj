using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie_project.Payloads.Requests;
using movie_project.Services.InterfaceService;

namespace movie_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaService _iCinemaService;
        public CinemaController(ICinemaService iCinemaService)
        {
            _iCinemaService = iCinemaService;
        }
        
        [HttpPut("AddCinema")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddCinema(CinemaRequest request)
        {
            return Ok(await _iCinemaService.AddCinema(request));
        }

        [HttpPost("UpdateCinemaInfo")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateCinemaInfo(int cinemaID, CinemaRequest request)
        {
            return Ok(await _iCinemaService.UpdateCinemaInfo(cinemaID, request));
        }

        [HttpPut("DeleteCinema")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteCinema(int cinemaID)
        {
            return Ok(await _iCinemaService.DeleteCinema(cinemaID));
        }
    }
}
