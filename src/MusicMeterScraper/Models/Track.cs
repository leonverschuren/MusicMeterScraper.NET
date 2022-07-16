namespace MusicMeterScraper.Models;

public class Track
{
    public string Title { get; internal set; }
    public IReadOnlyCollection<string> Artists { get; internal set; }
}
