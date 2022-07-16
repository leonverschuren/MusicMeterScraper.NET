using AngleSharp;
using AngleSharp.Dom;
using MusicMeterScraper.Models;
using MusicMeterScraper.Scrapers;

namespace MusicMeterScraper;

public class MusicMeterAlbumScraper : IDisposable
{
    private readonly IBrowsingContext _browsingContext;
    
    public MusicMeterAlbumScraper()
    {
        IConfiguration configuration = Configuration.Default.WithDefaultLoader();
        _browsingContext = BrowsingContext.New(configuration);
    }
    
    public async Task<Album> ScrapeAlbumByIdAsync(int id)
    {
        try
        {
            return await InternalScrapeAlbumById(id);
        }
        catch (Exception e)
        {
            throw new MusicMeterScraperException($"Exception while scraping album with id {id}", e);
        }
    }

    private async Task<Album> InternalScrapeAlbumById(int id)
    {
        using IDocument albumDocument = await _browsingContext.OpenAsync($"https://www.musicmeter.nl/album/{id}");
        var albumScraper = ScraperFactory.CreateAlbumScraper(albumDocument);
        
        using IDocument statsDocument = await _browsingContext.OpenAsync($"https://www.musicmeter.nl/album/{id}/stats");
        var statsScraper = new StatsScraper(statsDocument);

        return new Album
        {
            Title = albumScraper.ExtractTitle(),
            Cover = albumScraper.ExtractCover(),
            Artist = albumScraper.ExtractAlbumArtist(),
            Year = albumScraper.ExtractYear(),
            ReleaseDate = statsScraper.ExtractReleaseDate(),
            Rating = albumScraper.ExtractRating(),
            Label = albumScraper.ExtractLabel(),
            Genre = albumScraper.ExtractGenre(),
            Tracks = albumScraper.ExtractTracks().ToArray(),
            IsCompilation = albumScraper.IsCompilation
        };
    }

    public void Dispose()
    {
        _browsingContext.Dispose();
    }
}
