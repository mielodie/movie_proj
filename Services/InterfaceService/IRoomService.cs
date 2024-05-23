using movie_project.Payloads.DTOs;
using movie_project.Payloads.Requests;
using movie_project.Payloads.Requests.RoomRequests;
using movie_project.Payloads.Responses;

namespace movie_project.Services.InterfaceService
{
    public interface IRoomService
    {
        public Task<ResponseObject<RoomDTO>> AddRoom(int cinemaID, AddRoomRequest request);
        public Task<ResponseObject<RoomDTO>> UpdateRoomInfo(int roomID, UpdateRoomInfoRequest request);
        public Task<string> DeleteRoom(int roomID);
    }
}
