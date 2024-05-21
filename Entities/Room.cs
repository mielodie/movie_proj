namespace movie_project.Entities
{
    public class Room : BaseEntity
    {
        public int Capacity {  get; set; }
        public int Type {  get; set; }
        public string Description {  get; set; }
        public int CinemaID {  get; set; }
        public string Code {  get; set; }
        public string Name {  get; set; }
        public bool IsActive {  get; set; }
        public Cinema Cinema { get; set; }
        public IQueryable<Schedule> Schedules { get; set; }
        public IQueryable<Seat> Seats { get; set; }
    }
}
