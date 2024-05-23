using Microsoft.EntityFrameworkCore;
using movie_project.DataConection;
using movie_project.Entities;
using movie_project.Payloads.Converters;
using movie_project.Payloads.DTOs;
using movie_project.Payloads.Requests;
using movie_project.Payloads.Responses;
using movie_project.Services.InterfaceService;

namespace movie_project.Services
{
    public class CinemaServer : ICinemaService
    {
        private readonly AppDbContext _context;
        private readonly CinemaConverter _cinemaConverter;
        private readonly ResponseObject<CinemaDTO> _cinemaResponseObject;
        public CinemaServer()
        {
            _context = new AppDbContext();
            _cinemaConverter = new CinemaConverter();
            _cinemaResponseObject = new ResponseObject<CinemaDTO>();
        }
        public async Task<ResponseObject<CinemaDTO>> AddCinema(CinemaRequest request)
        {
            Cinema cinema = new Cinema
            {
                Address = request.Address,
                Code = request.Code,
                Description = request.Description,
                NameOfCinema = request.NameOfCinema,
                IsActive = true
            };
            await _context.AddAsync(cinema);
            await _context.SaveChangesAsync();
            return _cinemaResponseObject.ResponseObjectSuccess(
                "More successful cinemas",
                _cinemaConverter.ToDTO(cinema)
                );
        }

        public async Task<string> DeleteCinema(int cinemaID)
        {
            Cinema cinema = await _context.cinemas.SingleOrDefaultAsync(x => x.ID == cinemaID);
            if (cinema is null)
            {
                return "Cinema does not exist on the system";
            }
            cinema.IsActive = false;
            _context.cinemas.Update(cinema);
            await _context.SaveChangesAsync();
            return "Deleted cinema successfully";
        }

        public async Task<ResponseObject<CinemaDTO>> UpdateCinemaInfo(int cinemaID, CinemaRequest request)
        {
            Cinema cinema = await _context.cinemas.SingleOrDefaultAsync(x => x.ID == cinemaID);
            if (cinema is null)
            {
                return _cinemaResponseObject.ResponseObjectError(
                    StatusCodes.Status404NotFound,
                    "Cinema does not exist on the system",
                    null
                    );
            }
            cinema.NameOfCinema = request.NameOfCinema;
            cinema.Description = request.Description;
            cinema.Address = request.Address;   
            cinema.Code = request.Code;
            _context.cinemas.Update(cinema);
            await _context.SaveChangesAsync();
            return _cinemaResponseObject.ResponseObjectSuccess(
                "Updated cinema information successfully",
                _cinemaConverter.ToDTO(cinema)
                );
        }
    }
}
