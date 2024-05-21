namespace movie_project.Entities
{
    public class User : BaseEntity
    {
        public int Point {  get; set; }
        public string Username {  get; set; }
        public string Email {  get; set; }
        public string Name {  get; set; }
        public string PhoneNumber {  get; set; }
        public string Password {  get; set; }
        public int RankCustomerID {  get; set; }
        public int UserStatusID {  get; set; }
        public bool? IsActive {  get; set; }
        public int RoleID {  get; set; }
        public RankCustomer? RankCustomer { get; set; }
        public UserStatus? UserStatus { get; set; }
        public Role? Role { get; set; }
        public IQueryable<RefreshToken>? RefreshTokens { get; set; }
        public IQueryable<Bill>? Bills { get; set; }
        public IQueryable<ConfirmEmail>? ConfirmEmails { get; set; }
    }
}
