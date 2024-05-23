using movie_project.Entities;

namespace movie_project.Payloads.DTOs
{
    public class RoomDTO
    {
        public int Capacity { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public int CinemaID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
