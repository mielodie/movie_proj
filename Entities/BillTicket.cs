namespace movie_project.Entities
{
    public class BillTicket : BaseEntity
    {
        public int Quantity { get; set; }
        public int BillID { get; set; }
        public int TicketID { get; set; }
        public Bill Bill { get; set; }
        public Ticket Ticket { get; set; }
    }
}
