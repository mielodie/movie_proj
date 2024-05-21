namespace movie_project.Entities
{
    public class SeatStatus : BaseEntity
    {
        public string Code { get; set; }
        public string NameStatus { get; set; }
        public IQueryable<Seat> Seats { get; set; }
    }
}
