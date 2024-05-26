namespace movie_project.Payloads.Requests.TicketRequests
{
    public class UpdateTicketInfoRequest
    {
        public string Code { get; set; }
        public int ScheduleID { get; set; }
        public int SeatID { get; set; }
        public double PriceTicket { get; set; }
    }
}
