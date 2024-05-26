using movie_project.Payloads.DTOs;
using movie_project.Payloads.Requests.SeatRequests;
using movie_project.Payloads.Responses;

namespace movie_project.Services.InterfaceService
{
    public interface ISeatService
    {
        public Task<ResponseObject<SeatDTO>> AddSeatInRoom(int roomId, int seatStatusID, int seatTypeID, AddSeatInRoomRequest request);
    }
}
