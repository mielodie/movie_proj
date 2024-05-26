using movie_project.Payloads.DTOs;
using movie_project.Payloads.Requests.MovieRequests;
using movie_project.Payloads.Requests.ScheduleRequests;
using movie_project.Payloads.Responses;

namespace movie_project.Services.InterfaceService
{
    public interface IMovieService
    {
        public Task<ResponseObject<MovieDTO>> AddMovie(int movieTypeID, AddMovieRequest request, List<AddScheduleRequest> scheduleList);
        public Task<ResponseObject<MovieDTO>> UpdateMovieInfo(int movieID, UpdateMovieInfoRequest request, List<UpdateScheduleInfoRequest> scheduleList);
        public Task<string> DeleteMovie(int movieID);
    }
}
