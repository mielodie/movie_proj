namespace movie_project.Entities
{
    public class Promotion : BaseEntity
    {
        public int Percent { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int RankCustomerID { get; set; }
        public RankCustomer RankCustomer { get; set; }
        public IQueryable<Bill> Bills { get; set; }
    }
}
