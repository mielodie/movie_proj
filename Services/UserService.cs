using Microsoft.EntityFrameworkCore;
using movie_project.DataConection;
using movie_project.Entities;
using movie_project.Payloads.Converters;
using movie_project.Payloads.DTOs;
using movie_project.Payloads.Requests.UserRequests;
using movie_project.Payloads.Responses;
using movie_project.Services.InterfaceService;

namespace movie_project.Services
{

    public class UserService : IUserService
    {
        private readonly ResponseObject<UserDTO> _responseObjectUserDTO;
        private readonly AppDbContext _context;
        private readonly UserConverter _converter;
        public UserService()
        {
            _context = new AppDbContext();
            _responseObjectUserDTO = new ResponseObject<UserDTO>();
            _converter = new UserConverter();
        }
        public async Task<string> DeleteUser(int userID)
        {
            User user = await _context.users.SingleOrDefaultAsync(x => x.ID == userID);
            if (user == null)
            {
                return "The user does not exist on the system";
            }
            _context.Remove(user);
            _context.SaveChanges();
            return "User deletion successful";
        }

        public Task<IQueryable<UserDTO>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<UserDTO>> GetUsersByPhoneNumber()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<UserDTO>> GetUsersByUsername()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseObject<UserDTO>> UpdateUserInfo(int userID, UpdateUserInfoRequest request)
        {
            User user = await _context.users.SingleOrDefaultAsync(x => x.ID == userID);
            if (user == null)
            {
                return _responseObjectUserDTO.ResponseObjectError(
                    StatusCodes.Status404NotFound,
                    "The user does not exist on the system",
                    null
                );
            }
            user.PhoneNumber = request.PhoneNumber;
            user.Name = request.Name;
            user.Username = request.Username;
            if (await _context.users.SingleOrDefaultAsync(x => x.Username == request.Username) != null)
            {
                return _responseObjectUserDTO.ResponseObjectError(
                    StatusCodes.Status400BadRequest,
                    "This username already exists on the system",
                    null
                );
            }
            _context.Update(user);
            _context.SaveChanges();
            return _responseObjectUserDTO.ResponseObjectSuccess(
                    "User information updated successfully",
                    _converter.ToDTO(user)
                );
        }
    }
}
