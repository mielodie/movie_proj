namespace movie_project.Payloads.Requests.UserRequests
{
    public class CreateNewPasswordRequest
    {
        public string Verification { get; set; }
        public string NewPassword { get; set; }
    }
}
