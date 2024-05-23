using movie_project.Entities;
using movie_project.Payloads.DTOs;

namespace movie_project.Payloads.Converters
{
    public class RoomConverter
    {
        public RoomDTO ToDTO(Room room)
        {
            return new RoomDTO
            {
                Capacity = room.Capacity,
                CinemaID = room.CinemaID,
                Code = room.Code,
                Description = room.Description,
                IsActive = room.IsActive,
                Name = room.Name,
                Type = room.Type
            };
        }
    }
}
