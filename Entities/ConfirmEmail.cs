namespace movie_project.Entities
{
    public class ConfirmEmail : BaseEntity
    {
        public int UserID { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string ConfirmCode { get; set; }
        public bool IsConfirm { get; set; }
        public User User { get; set; }
    }
}
