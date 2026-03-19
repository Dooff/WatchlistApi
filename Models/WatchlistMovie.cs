namespace WatchlistApi.Models
{
    public class WatchlistMovie
    {
        public int WatchlistId { get; set; }
        public Watchlist Watchlist { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}