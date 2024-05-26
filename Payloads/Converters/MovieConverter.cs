using FluentEmail.Core;
using movie_project.DataConection;
using movie_project.Entities;
using movie_project.Payloads.DTOs;

namespace movie_project.Payloads.Converters
{
    public class MovieConverter
    {
        private readonly AppDbContext _context;
        private readonly ScheduleConverter _scheduleConverter;

        public MovieConverter()
        {
            _context = new AppDbContext();
            _scheduleConverter = new ScheduleConverter();
        }

        public MovieDTO ToDTO(Movie movie)
        {
            return new MovieDTO
            {
                Description = movie.Description,
                Director = movie.Director,
                EndTime = movie.EndTime,
                HeroImage = movie.HeroImage,
                Image = movie.Image,
                Language = movie.Language,
                MovieDuration = movie.MovieDuration,
                MovieTypeName = movie.MovieType.MovieTypeName,
                Name = movie.Name,
                PremiereDate = movie.PremiereDate,
                Trailer = movie.Trailer,
                ScheduleDTOs = _scheduleConverter.ToScheduleDTOList(movie.Schedules)
            };
        }

    }
}
