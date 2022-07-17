namespace MusicMeterScraper.Models;

public class Track
{
    public int TrackNumber { get; internal set; }
    public string Title { get; internal set; }
    public IReadOnlyCollection<string> Artists { get; internal set; }
}
