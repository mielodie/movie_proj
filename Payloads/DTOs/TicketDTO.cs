namespace movie_project.Payloads.DTOs
{
    public class TicketDTO
    {
        public string Code { get; set; }
        public int ScheduleID { get; set; }
        public int SeatID { get; set; }
        public double PriceTicket { get; set; }
        public bool IsActive { get; set; }
    }
}
