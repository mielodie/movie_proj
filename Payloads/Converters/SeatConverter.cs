using movie_project.DataConection;
using movie_project.Entities;
using movie_project.Payloads.DTOs;

namespace movie_project.Payloads.Converters
{
    public class SeatConverter
    {
        private readonly AppDbContext _context;

        public SeatConverter()
        {
            _context = new AppDbContext();
        }
        public SeatDTO ToDTO(Seat seat)
        {
            return new SeatDTO
            {
                Line = seat.Line,
                Number = seat.Number,
                RoomID = seat.RoomID,
                SeatStatusName = _context.seatStatus.SingleOrDefault(x => x.ID == seat.SeatStatusID).NameStatus,
                SeatTypeName = _context.seatTypes.SingleOrDefault(x => x.ID == seat.SeatTypeID).NameType
            };

        }
    }
}
