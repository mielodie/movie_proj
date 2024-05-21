namespace movie_project.Entities
{
    public class BillStatus : BaseEntity
    {
        public string Name {  get; set; }
        public IQueryable<Bill> Bills { get; set; }
    }
}
