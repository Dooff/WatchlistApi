using System.Collections.Generic;

namespace WatchlistApi.DTOs
{
    public class WatchlistDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MovieDto> Movies { get; set; } = new List<MovieDto>();
    }

    public class WatchlistCreateDto
    {
        public string Name { get; set; }
    }

    public class WatchlistAddMovieDto
    {
        public int WatchlistId { get; set; }
        public int MovieId { get; set; }
    }
}
