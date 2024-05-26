using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;
using movie_project.DataConection;
using movie_project.Entities;
using movie_project.Payloads.Converters;
using movie_project.Payloads.DTOs;
using movie_project.Payloads.Requests.MovieRequests;
using movie_project.Payloads.Requests.ScheduleRequests;
using movie_project.Payloads.Responses;
using movie_project.Services.InterfaceService;
using System.Collections.Generic;

namespace movie_project.Services
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _context;
        private readonly MovieConverter _movieConverter;
        private readonly ScheduleConverter _scheduleConverter;
        private readonly ResponseObject<MovieDTO> _responseObjectMovieDTO;

        public MovieService()
        {
            _context = new AppDbContext();
            _movieConverter = new MovieConverter();
            _scheduleConverter = new ScheduleConverter();
            _responseObjectMovieDTO = new ResponseObject<MovieDTO>();
        }

        public async Task<ResponseObject<MovieDTO>> AddMovie(int movieTypeID, AddMovieRequest request, List<AddScheduleRequest> scheduleList)
        {
            MovieType movieType = await _context.movieTypes.SingleOrDefaultAsync(x => x.ID == movieTypeID);
            if(movieType is null)
            {
                return _responseObjectMovieDTO.ResponseObjectError(
                    StatusCodes.Status404NotFound,
                    "Movie type does not exist on the system",
                    null
                    );
            }

            Movie movie = new Movie();

            movie.Description = request.Description;
            movie.Director = request.Director;
            movie.EndTime = request.EndTime;
            movie.HeroImage = request.HeroImage;
            movie.Image = request.Image;
            movie.Language = request.Language;
            movie.IsActive = true;
            movie.MovieDuration = request.MovieDuration;
            movie.MovieTypeID = movieTypeID;
            movie.Name = request.Name;
            movie.PremiereDate = request.PremiereDate;
            movie.Trailer = request.Trailer;
            movie.RateID = 1;
            movie.Schedules = AddSchedules(movie.ID, scheduleList).AsQueryable();

            await _context.movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return _responseObjectMovieDTO.ResponseObjectSuccess(
                "More successful movies",
                _movieConverter.ToDTO(movie)
                );
        }

        public async Task<string> DeleteMovie(int movieID)
        {
            Movie movie = await _context.movies.SingleOrDefaultAsync(x => x.ID == movieID);
            if (movie is null)
            {
                return "Movie does not exist on the system";
            }
            movie.IsActive = false;
            _context.movies.Update(movie);
            await _context.SaveChangesAsync();
            return "Deleted movie successfully";
        }

        public async Task<ResponseObject<MovieDTO>> UpdateMovieInfo(int movieID, UpdateMovieInfoRequest request, List<UpdateScheduleInfoRequest> scheduleList)
        {
            Movie movie = await _context.movies.SingleOrDefaultAsync(x => x.ID == movieID);
            if (movie is null)
            {
                return _responseObjectMovieDTO.ResponseObjectError(
                    StatusCodes.Status404NotFound,
                    "Movie does not exist on the system",
                    null
                    );
            }
            movie.Description = request.Description;
            movie.Director = request.Director;
            movie.EndTime = request.EndTime;
            movie.HeroImage = request.HeroImage;
            movie.Image = request.Image;
            movie.Language = request.Language;
            movie.IsActive = true;
            movie.MovieDuration = request.MovieDuration;
            movie.MovieTypeID = request.MovieTypeID;
            movie.Name = request.Name;
            movie.PremiereDate = request.PremiereDate;
            movie.Trailer = request.Trailer;

            if(movie.Schedules.Count() != 0)
            {
                movie.Schedules.ForEach(x =>
                {
                    _context.schedules.Remove(x);
                });
                await _context.SaveChangesAsync();
            }

            movie.Schedules = UpdateSchedules(movieID, scheduleList).AsQueryable();

            _context.movies.Update(movie);
            await _context.SaveChangesAsync();

            return _responseObjectMovieDTO.ResponseObjectSuccess(
                "Updated movie information successfully",
                _movieConverter.ToDTO(movie)
                );
        }

        private List<Schedule> UpdateSchedules(int movieID, List<UpdateScheduleInfoRequest> requests)
        {
            List<Schedule> schedules = _context.schedules.Where(x => x.MovieID == movieID).ToList();
            schedules.ForEach(x =>
            {
                requests.ForEach(y =>
                {
                    x.EndAt = y.EndAt;
                    x.StartAt = y.StartAt;
                    x.IsActive = true;
                });
            });
            return schedules;
        }

        private List<Schedule> AddSchedules(int movieID, List<AddScheduleRequest> requests)
        {
            List<Schedule> schedules = _context.schedules.Where(x => x.MovieID == movieID).ToList();
            schedules.ForEach(x =>
            {
                requests.ForEach(y =>
                {
                    x.RoomID = y.RoomID;
                    x.EndAt = y.EndAt;
                    x.StartAt = y.StartAt;
                    x.IsActive = true;
                });
            });
            return schedules;
        }
    }
}
