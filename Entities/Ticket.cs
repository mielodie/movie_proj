namespace movie_project.Entities
{
    public class Ticket : BaseEntity
    {
        public string Code { get; set; }
        public int ScheduleID { get; set; }
        public int SeatID { get; set; }
        public double PriceTicket { get; set; }
        public bool IsActive { get; set; }
        public Schedule Schedule { get; set; }
        public Seat Seat { get; set; }
        public IQueryable<BillTicket> BillTickets { get; set; }
    }
}
