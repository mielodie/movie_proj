using movie_project.Payloads.DTOs;
using movie_project.Payloads.Requests.UserRequests;
using movie_project.Payloads.Responses;

namespace movie_project.Services.InterfaceService
{
    public interface IUserService
    {
        public Task<ResponseObject<UserDTO>> UpdateUserInfo(int userID, UpdateUserInfoRequest request);
        public Task<string> DeleteUser(int userID);
        Task<IQueryable<UserDTO>> GetAllUsers();
        Task<IQueryable<UserDTO>> GetUsersByUsername();
        Task<IQueryable<UserDTO>> GetUsersByPhoneNumber();

    }
}
