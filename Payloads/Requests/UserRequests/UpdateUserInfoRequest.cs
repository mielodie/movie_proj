using movie_project.Payloads.DTOs;

namespace movie_project.Payloads.Requests.UserRequests
{
    public class UpdateUserInfoRequest
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
