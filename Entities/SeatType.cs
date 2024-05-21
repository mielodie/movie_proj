namespace movie_project.Entities
{
    public class SeatType : BaseEntity
    {
        public string Code { get; set; }
        public string NameType { get; set; }
        public IQueryable<Seat> Seats { get; set; }
    }
}
