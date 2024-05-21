using movie_project.Entities;
using movie_project.Payloads.DTOs;

namespace movie_project.Payloads.Converters
{
    public class UserConverter
    {
        public UserDTO ToDTO(User user) { 
            return new UserDTO
            {
                Email = user.Email,
                IsActive = user.IsActive,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Point = user.Point,
                RankCustomerID = user.RankCustomerID,
                RoleID = user.RoleID,
                Username = user.Username,
                UserStatusID = user.UserStatusID
            }; 
        }
    }
}
