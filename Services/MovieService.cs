using WatchlistApi.Data;
using WatchlistApi.Models;
using WatchlistApi.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchlistApi.Services
{
    public class MovieService
    {
        private readonly AppDbContext _context;

        public MovieService(AppDbContext context)
        {
            _context = context;
        }

        //Devuelve todas las pelis opcionalmente filtradas 
        public async Task<List<Movie>> GetMoviesAsync(MovieDto filter)
        {
            var query = _context.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(m => m.Title.ToLower().Contains(filter.Title.ToLower()));

            if (filter.Year != null)
                query = query.Where(m => m.Year == filter.Year);

            if (!string.IsNullOrWhiteSpace(filter.Category))
                query = query.Where(m => m.Category == filter.Category);

            return await query
                .AsNoTracking()
                .ToListAsync();
        }

        // Devuelve una peli por Id
        public async Task<Movie?> GetMovieByIdAsync(int movieId)
        {
            return await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == movieId);
        }
    }
}