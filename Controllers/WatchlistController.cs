using Microsoft.AspNetCore.Mvc;
using WatchlistApi.DTOs;
using WatchlistApi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WatchlistApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WatchlistsController : ControllerBase
    {
        private readonly WatchlistService _watchlistService;

        public WatchlistsController(WatchlistService watchlistService)
        {
            _watchlistService = watchlistService;
        }

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WatchlistDto>>> GetWatchlists()
        {
            var userId = GetUserId();
            var watchlists = await _watchlistService.GetUserWatchlistsAsync(userId);

            var dto = watchlists.Select(w => new WatchlistDto
            {
                Id = w.Id,
                Name = w.Name,
                Movies = w.WatchlistMovies.Select(wm => new MovieDto
                {
                    Id = wm.Movie.Id,
                    Title = wm.Movie.Title,
                    Category = wm.Movie.Category,
                    Year = wm.Movie.Year
                }).ToList()
            }).ToList();

            return Ok(dto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<WatchlistDto>> GetWatchlistById(int id)
        {
            var userId = GetUserId();
            var watchlist = await _watchlistService.GetUserWatchlistByIdAsync(userId, id);

            if (watchlist == null)
                return NotFound();

            var dto = new WatchlistDto
            {
                Id = watchlist.Id,
                Name = watchlist.Name,
                Movies = watchlist.WatchlistMovies.Select(wm => new MovieDto
                {
                    Id = wm.Movie.Id,
                    Title = wm.Movie.Title,
                    Category = wm.Movie.Category,
                    Year = wm.Movie.Year
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<WatchlistDto>> CreateWatchlist(WatchlistCreateDto dto)
        {
            var userId = GetUserId();
            var watchlist = await _watchlistService.CreateWatchlistAsync(userId, dto.Name);

            return Ok(new WatchlistDto { Id = watchlist.Id, Name = watchlist.Name });
        }

        [HttpPost("add-movie")]
        public async Task<IActionResult> AddMovieToWatchlist(WatchlistAddMovieDto dto)
        {
            await _watchlistService.AddMovieAsync(dto.WatchlistId, dto.MovieId);
            return Ok();
        }

        [HttpPost("remove-movie")]
        public async Task<IActionResult> RemoveMovieFromWatchlist(WatchlistAddMovieDto dto)
        {
            await _watchlistService.RemoveMovieAsync(dto.WatchlistId, dto.MovieId);
            return Ok();
        }
    }
}