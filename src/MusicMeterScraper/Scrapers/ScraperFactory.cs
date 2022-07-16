using AngleSharp.Dom;

namespace MusicMeterScraper.Scrapers;

internal static class ScraperFactory
{
    public static IAlbumScraper CreateAlbumScraper(IDocument document)
    {
        IElement header = document.QuerySelector("h1 a");

        return header != null 
            ? new RegularAlbumScraper(document) 
            : new CompilationAlbumScraper(document);
    }
}
