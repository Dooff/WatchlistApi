using WatchlistApi.Data;
using WatchlistApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchlistApi.Services
{
    public class WatchlistService
    {
        private readonly AppDbContext _context;

        public WatchlistService(AppDbContext context)
        {
            _context = context;
        }

        //Obtiene todas las listas de un usuario, con sus películas
        public async Task<List<Watchlist>> GetUserWatchlistsAsync(int userId)
        {
            return await _context.Watchlists
                .Where(w => w.UserId == userId)
                .Include(w => w.WatchlistMovies)
                    .ThenInclude(wm => wm.Movie)
                .AsNoTracking()
                .ToListAsync();
        }
        //Obtiene una watchlist específica de un usuario, con sus películas
        public async Task<Watchlist?> GetUserWatchlistByIdAsync(int userId, int watchlistId)
        {
            return await _context.Watchlists
                .Where(w => w.UserId == userId && w.Id == watchlistId)
                .Include(w => w.WatchlistMovies)
                    .ThenInclude(wm => wm.Movie)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        //Crea una nueva lista
        public async Task<Watchlist> CreateWatchlistAsync(int userId, string name)
        {
            var watchlist = new Watchlist
            {
                Name = name,
                UserId = userId
            };
            _context.Watchlists.Add(watchlist);
            await _context.SaveChangesAsync();
            return watchlist;
        }

        //Añade una peli a la lista
        public async Task AddMovieAsync(int watchlistId, int movieId)
        {
            var exists = await _context.WatchlistMovies
                .AnyAsync(wm => wm.WatchlistId == watchlistId && wm.MovieId == movieId);

            if (!exists)
            {
                _context.WatchlistMovies.Add(new WatchlistMovie
                {
                    WatchlistId = watchlistId,
                    MovieId = movieId
                });
                await _context.SaveChangesAsync();
            }
        }

        //Elimina una peli de la lista
        public async Task RemoveMovieAsync(int watchlistId, int movieId)
        {
            var watchlistMovie = await _context.WatchlistMovies
                .FirstOrDefaultAsync(wm => wm.WatchlistId == watchlistId && wm.MovieId == movieId);

            if (watchlistMovie != null)
            {
                _context.WatchlistMovies.Remove(watchlistMovie);
                await _context.SaveChangesAsync();
            }
        }
    }
}