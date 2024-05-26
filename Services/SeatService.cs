using Microsoft.EntityFrameworkCore;
using movie_project.DataConection;
using movie_project.Entities;
using movie_project.Payloads.Converters;
using movie_project.Payloads.DTOs;
using movie_project.Payloads.Requests.SeatRequests;
using movie_project.Payloads.Responses;
using movie_project.Services.InterfaceService;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace movie_project.Services
{
    public class SeatService : ISeatService
    {
        private readonly AppDbContext _context;
        private readonly SeatConverter _seatConverter;
        private readonly ResponseObject<SeatDTO> _responseObjectSeatDTO;
        public SeatService()
        {
            _context = new AppDbContext();
            _seatConverter = new SeatConverter();
            _responseObjectSeatDTO = new ResponseObject<SeatDTO>();
        }
        public async Task<ResponseObject<SeatDTO>> AddSeatInRoom(int roomId, int seatStatusID, int seatTypeID, AddSeatInRoomRequest request)
        {
            Room room = await _context.rooms.SingleOrDefaultAsync(x => x.ID == roomId);
            if(room is null)
            {
                return _responseObjectSeatDTO.ResponseObjectError(
                    StatusCodes.Status404NotFound,
                    "This room does not exist on the system",
                    null
                    );
            }
            SeatStatus seatStatus = await _context.seatStatus.SingleOrDefaultAsync(x => x.ID == seatStatusID);
            if (room is null)
            {
                return _responseObjectSeatDTO.ResponseObjectError(
                    StatusCodes.Status404NotFound,
                    "This seat's status does not exist on the system",
                    null
                    );
            }
            SeatType seatType = await _context.seatTypes.SingleOrDefaultAsync(x => x.ID == seatTypeID);
            if (room is null)
            {
                return _responseObjectSeatDTO.ResponseObjectError(
                    StatusCodes.Status404NotFound,
                    "This seat's type does not exist on the system",
                    null
                    );
            }

            Seat seat = new Seat();
            seat.Number = request.Number;
            seat.Line = request.Line;
            seat.SeatStatusID = seatStatusID;
            seat.SeatTypeID = seatTypeID;
            seat.IsActive = true;
            seat.RoomID = roomId;

            await _context.seats.AddAsync(seat);
            await _context.SaveChangesAsync();

            return _responseObjectSeatDTO.ResponseObjectSuccess(
                "More successful seats",
                _seatConverter.ToDTO(seat)
                );
        }
    }
}
