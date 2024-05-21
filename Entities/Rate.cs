namespace movie_project.Entities
{
    public class Rate : BaseEntity
    {
        public string Description { get; set; }
        public string Code { get; set; }
        public IQueryable<Movie> Movies { get; set; }
    }
}
