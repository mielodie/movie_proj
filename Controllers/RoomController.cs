using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie_project.Payloads.Requests;
using movie_project.Payloads.Requests.RoomRequests;
using movie_project.Services.InterfaceService;

namespace movie_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _iRoomService;
        public RoomController(IRoomService iRoomService)
        {
            _iRoomService = iRoomService;
        }

        [HttpPut("AddRoom")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddRoom(int cinemaID, AddRoomRequest request)
        {
            return Ok(await _iRoomService.AddRoom(cinemaID, request));
        }

        [HttpPost("UpdateRoomInfo")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateRoomInfo(int roomID, UpdateRoomInfoRequest request)
        {
            return Ok(await _iRoomService.UpdateRoomInfo(roomID, request));
        }

        [HttpPut("DeleteRoom")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteRoom(int roomID)
        {
            return Ok(await _iRoomService.DeleteRoom(roomID));
        }
    }
}
