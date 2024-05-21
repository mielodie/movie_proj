namespace movie_project.Entities
{
    public class MovieType : BaseEntity
    {
        public string MovieTypeName { get; set; }
        public bool IsActive { get; set; }
        public IQueryable<Movie> Movies { get; set; }
    }
}
