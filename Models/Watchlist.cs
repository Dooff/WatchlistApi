namespace WatchlistApi.Models
{
    public class Watchlist
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<WatchlistMovie> WatchlistMovies { get; set; }
}
}