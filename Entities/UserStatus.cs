namespace movie_project.Entities
{
    public class UserStatus : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public IQueryable<User> Users { get; set; }
    }
}
