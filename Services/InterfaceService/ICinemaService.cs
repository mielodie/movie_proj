using movie_project.Payloads.DTOs;
using movie_project.Payloads.Requests;
using movie_project.Payloads.Responses;

namespace movie_project.Services.InterfaceService
{
    public interface ICinemaService
    {
        public Task<ResponseObject<CinemaDTO>> AddCinema(CinemaRequest request);
        public Task<ResponseObject<CinemaDTO>> UpdateCinemaInfo(int cinemaID, CinemaRequest request);
        public Task<string> DeleteCinema(int cinemaID);
    }
}
