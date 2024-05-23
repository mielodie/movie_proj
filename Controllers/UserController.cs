using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movie_project.Payloads.Requests.UserRequests;
using movie_project.Services.InterfaceService;

namespace movie_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase 
    {
        private readonly IUserService _iUserService;
        public UserController(IUserService iUserService)
        {
            _iUserService = iUserService;
        }
        [HttpPut("UpdateUserInfo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateUserInfo(UpdateUserInfoRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!int.TryParse(HttpContext.User.FindFirst("Id")?.Value, out int id))
                {
                    return BadRequest("Invalid user id");
                }

                var result = await _iUserService.UpdateUserInfo(id, request);

                if (result.Message.ToLower().Contains("User information updated successfully".ToLower()))
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("DeleteUser")]
        [Authorize(Roles = "ADMIN, STAFF")]
        public async Task<IActionResult> DeleteUser(int userID)
        {
            return Ok(await _iUserService.DeleteUser(userID));
        }
        
    }
}
