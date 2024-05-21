using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie_project.Payloads.DTOs;
using movie_project.Payloads.Requests.UserRequests;
using movie_project.Services.InterfaceService;

namespace movie_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _iAuthService;
        public AuthController(IAuthService iAuthService)
        {
            _iAuthService = iAuthService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterAccountRequest request)
        {
            return Ok(await _iAuthService.Register(request));
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginAccountRequest request)
        {
            return Ok(await _iAuthService.Login(request));
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotAccountPasswordRequest request)
        {
            return Ok(await _iAuthService.ForgotPassword(request));
        }
        [HttpPost("CreateNewPassword")]
        public async Task<IActionResult> CreateNewPassword(CreateNewPasswordRequest request)
        {
            return Ok(await _iAuthService.CreateNewPassword(request));
        }
        [HttpPut]
        [Route("ChangePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
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

                var result = await _iAuthService.ChangePassword(id, request);

                if (result.ToLower().Contains("Password changed successfully".ToLower()))
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
        [HttpPut("SetRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetRole(int userID)
        {
            return Ok(await _iAuthService.SetRole(userID));
        }
        [HttpPost]
        [Route("RenewToken")]
        public IActionResult RenewToken(RefreshTokenDTO token)
        {
            var result = _iAuthService.RenewAccessToken(token);
            if (result == null)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
    }
}
