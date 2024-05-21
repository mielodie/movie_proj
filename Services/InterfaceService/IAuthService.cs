using Microsoft.AspNetCore.Identity.Data;
using movie_project.Entities;
using movie_project.ImageAndEmail.Email;
using movie_project.Payloads.DTOs;
using movie_project.Payloads.Requests.UserRequests;
using movie_project.Payloads.Responses;


namespace movie_project.Services.InterfaceService
{
    public interface IAuthService
    {
        Task<ResponseObject<UserDTO>> Register(RegisterAccountRequest request);
        RefreshTokenDTO GenerateAccessToken(User user);
        string GenerateRefreshToken();
        Task<ResponseObject<RefreshTokenDTO>> Login(LoginAccountRequest request);
        Task<string> ForgotPassword(ForgotAccountPasswordRequest request);
        Task<ResponseObject<UserDTO>> CreateNewPassword(CreateNewPasswordRequest request);
        string SendEmail(EmailTo emailTo);
        Task<string> ChangePassword(int userID, ChangePasswordRequest request);
        Task<string> SetRole(int userID);
        ResponseObject<RefreshTokenDTO> RenewAccessToken(RefreshTokenDTO request);
    }
}
