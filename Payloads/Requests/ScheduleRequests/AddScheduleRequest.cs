namespace movie_project.Payloads.Requests.ScheduleRequests
{
    public class AddScheduleRequest
    {
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int RoomID { get; set; }
    }
}
