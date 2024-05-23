namespace movie_project.Payloads.Requests.RoomRequests
{
    public class UpdateRoomInfoRequest
    {
        public int Capacity { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public int CinemaID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
