namespace movie_project.Payloads.Requests.UserRequests
{
    public class RegisterAccountRequest
    {
        public int Point { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int RankCustomerID { get; set; }
        public int UserStatusID { get; set; }
        public bool IsActive { get; set; }
        public int RoleID { get; set; }
    }
}
