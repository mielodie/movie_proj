using movie_project.Entities;
using movie_project.Payloads.DTOs;

namespace movie_project.Payloads.Converters
{
    public class CinemaConverter
    {
        public CinemaDTO ToDTO(Cinema cinema)
        {
            return new CinemaDTO
            {
                Address = cinema.Address,
                Code = cinema.Code,
                Description = cinema.Description,
                NameOfCinema = cinema.NameOfCinema,
                IsActive = cinema.IsActive
            };
        }
    }
}
