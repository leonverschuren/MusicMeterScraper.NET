#nullable enable
namespace MusicMeterScraper.Models;

public class Album
{
    public string Title { get; internal set; }
    public string Cover { get; internal set; }
    public string? Artist { get; internal set; }
    public int Year { get; internal set; }
    public DateTime? ReleaseDate { get; internal set; }
    public decimal Rating { get; internal set; }
    public string Label { get; internal set; }
    public string Genre { get; internal set; }
    public IReadOnlyCollection<Track> Tracks { get; internal set; }
    public bool IsCompilation { get; internal set; }
}
