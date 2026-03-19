using Microsoft.EntityFrameworkCore;
using WatchlistApi.Models;


namespace WatchlistApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<WatchlistMovie> WatchlistMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WatchlistMovie>()
                .HasKey(wm => new { wm.WatchlistId, wm.MovieId });
        }
    }
}
