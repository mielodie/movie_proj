namespace movie_project.Payloads.DTOs
{
    public class ScheduleDTO
    {
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int RoomID { get; set; }
        public bool IsActive { get; set; }
    }
}
