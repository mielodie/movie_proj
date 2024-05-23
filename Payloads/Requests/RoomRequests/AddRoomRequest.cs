using movie_project.Entities;

namespace movie_project.Payloads.Requests.RoomRequests
{
    public class AddRoomRequest
    {
        public int Capacity { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
