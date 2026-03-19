using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WatchlistApi.DTOs;
using WatchlistApi.Services;

namespace WatchlistApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies([FromQuery] MovieDto filter)
        {
            //Movemos la lógica al service 
            var movies = await _movieService.GetMoviesAsync(filter);
            var dto = movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Category = m.Category,
                Year = m.Year
            }).ToList();

            return Ok(dto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovieById(int id)
        {
            //Movemos la lógica al service 
            var movie = await _movieService.GetMovieByIdAsync(id);

            if (movie == null)
                return NotFound();
            var dto = new MovieDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Category = movie.Category,
                    Year = movie.Year
                };

            return Ok(dto);
        }
    }
}