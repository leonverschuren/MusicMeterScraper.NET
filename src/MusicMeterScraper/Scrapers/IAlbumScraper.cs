using MusicMeterScraper.Models;

namespace MusicMeterScraper.Scrapers;

internal interface IAlbumScraper
{
    bool IsCompilation { get; }
    string ExtractTitle();
    string ExtractAlbumArtist();
    string ExtractCover();
    int ExtractYear();
    decimal? ExtractRating();
    string ExtractGenre();
    string ExtractLabel();
    IEnumerable<Track> ExtractTracks();
}
