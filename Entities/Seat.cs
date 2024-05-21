namespace movie_project.Entities
{
    public class Seat : BaseEntity
    {
        public int Number {  get; set; }
        public int SeatStatusID {  get; set; }
        public string Line {  get; set; }
        public int RoomID {  get; set; }
        public bool IsActive {  get; set; }
        public int SeatTypeID {  get; set; }
        public SeatStatus SeatStatus { get; set; }
        public Room Room { get; set; }
        public SeatType SeatType { get; set; }
        public IQueryable<Ticket> Tickets { get; set; }
    }
}
