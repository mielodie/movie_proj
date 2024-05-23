using Microsoft.EntityFrameworkCore;
using movie_project.DataConection;
using movie_project.Entities;
using movie_project.Payloads.Converters;
using movie_project.Payloads.DTOs;
using movie_project.Payloads.Requests.RoomRequests;
using movie_project.Payloads.Responses;
using movie_project.Services.InterfaceService;

namespace movie_project.Services
{
    public class RoomService : IRoomService
    {
        private readonly AppDbContext _context;
        private readonly RoomConverter _roomConverter;
        private readonly ResponseObject<RoomDTO> _responseObjectRoomDTO;

        public RoomService()
        {
            _context = new AppDbContext();
            _roomConverter = new RoomConverter();
            _responseObjectRoomDTO = new ResponseObject<RoomDTO>();
        }

        public async Task<ResponseObject<RoomDTO>> AddRoom(int cinemaID, AddRoomRequest request)
        {
            Cinema cinema = await _context.cinemas.SingleOrDefaultAsync(x => x.ID == cinemaID);
            if(cinema is null)
            {
                return _responseObjectRoomDTO.ResponseObjectError(
                    StatusCodes.Status404NotFound,
                    "Cinema does not exist on the system",
                    null
                    );
            }
            Room room = new Room
            {
                Capacity = request.Capacity,
                CinemaID = cinemaID,
                Description = request.Description,
                Type = request.Type,
                Name = request.Name,
                Code = request.Code,
                IsActive = true
            };
            await _context.rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            return _responseObjectRoomDTO.ResponseObjectSuccess(
                "More successful room",
                _roomConverter.ToDTO(room)
                );
        }

        public async Task<string> DeleteRoom(int roomID)
        {
            Room room = await _context.rooms.SingleOrDefaultAsync(x => x.ID == roomID);
            if (room is null)
            {
                return "Room does not exist on the system";
            }
            room.IsActive = false;
            _context.rooms.Update(room);
            await _context.SaveChangesAsync();
            return "Deleted room successfully";
        }

        public async Task<ResponseObject<RoomDTO>> UpdateRoomInfo(int roomID, UpdateRoomInfoRequest request)
        {
            Room checkRoom = await _context.rooms.SingleOrDefaultAsync( x => x.ID == roomID);
            if(checkRoom is null)
            {
                return _responseObjectRoomDTO.ResponseObjectError(
                    StatusCodes.Status404NotFound,
                    "This room does not exist on the system",
                    null
                    );
            }
            checkRoom.Name = request.Name;
            checkRoom.Code = request.Code;
            checkRoom.Description = request.Description;
            checkRoom.Capacity = request.Capacity;
            checkRoom.Type = request.Type;
            checkRoom.CinemaID = request.CinemaID;
            if(await _context.cinemas.SingleOrDefaultAsync(x => x.ID == request.CinemaID) is null)
            {
                return _responseObjectRoomDTO.ResponseObjectError(
                    StatusCodes.Status404NotFound,
                    "Cinema does not exist on the system",
                    null
                    );
            }
            _context.rooms.Update(checkRoom);
            await _context.SaveChangesAsync();
            return _responseObjectRoomDTO.ResponseObjectSuccess(
                "Updated room information successfully",
                _roomConverter.ToDTO(checkRoom)
                );
        }
    }
}
