using movie_project.Entities;
using movie_project.Payloads.DTOs;

namespace movie_project.Payloads.Converters
{
    public class TicketConverter
    {
        public TicketDTO ToDTO(Ticket ticket)
        {
            return new TicketDTO
            {
                Code = ticket.Code,
                IsActive = ticket.IsActive,
                PriceTicket = ticket.PriceTicket,
                ScheduleID = ticket.ScheduleID,
                SeatID = ticket.SeatID,
            };
        }
    }
}
