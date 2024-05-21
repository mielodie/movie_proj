namespace movie_project.Entities
{
    public class Role : BaseEntity
    {
        public string Code { get; set; }
        public string RoleName { get; set; }
        public IQueryable<User> Users { get; set; }
    }
}
