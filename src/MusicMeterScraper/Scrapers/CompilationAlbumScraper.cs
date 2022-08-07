using AngleSharp.Dom;

namespace MusicMeterScraper.Scrapers;

internal class CompilationAlbumScraper : RegularAlbumScraper
{
    public CompilationAlbumScraper(IDocument document) : base(document)
    {
    }

    public override bool IsCompilation => true;

    public override string ExtractTitle()
    {
        return Document.QuerySelector("#main div.details > h1 > span").Text().Trim();
    }

    public override string ExtractAlbumArtist()
    {
        return null;
    }
}
