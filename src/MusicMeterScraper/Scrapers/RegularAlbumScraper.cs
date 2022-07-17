using System.Globalization;
using AngleSharp.Dom;
using MusicMeterScraper.Models;

namespace MusicMeterScraper.Scrapers;

internal class RegularAlbumScraper : IAlbumScraper
{
    protected readonly IDocument Document;

    public RegularAlbumScraper(IDocument document)
    {
        Document = document;
    }

    public virtual bool IsCompilation { get; } = false;

    public virtual string ExtractTitle()
    {
        var title = Document.QuerySelector("#main div.details > h1 > span").Text();
        return title.Substring(title.IndexOf(" - ") + 3);
    }

    public virtual string ExtractAlbumArtist()
    {
        var element = Document.QuerySelector("#main div.details > h1 > span > a");
        return element.Text().Trim();
    }

    public string ExtractCover()
    {
        var element = Document.QuerySelector("#main div.image > img");
        string url = element.Attributes["src"].Value;

        int index = url.IndexOf("?", StringComparison.Ordinal);
        if (index != -1)
        {
            url = url.Substring(0, url.IndexOf("?", StringComparison.Ordinal));
        }

        return url.Replace(".300", "");
    }

    public int ExtractYear()
    {
        var element = Document.QuerySelector("#main div.details > h1");
        string year = element.Text();
        return int.Parse(year.Substring(year.LastIndexOf("(") + 1, 4));
    }

    public decimal? ExtractRating()
    {
        if (Document.QuerySelector(".star-rating.entity-rating.not-voted") is not null)
        {
            return null;
        }
        
        var element = Document.QuerySelector("#main div.star-rating.entity-rating > span");
        return decimal.Parse(element.Text(), new CultureInfo("nl-NL"));
    }

    public string ExtractGenre()
    {
        var element = Document.QuerySelector(".details-inner > p");
        return element.ChildNodes[2].Text().Trim();
    }

    public string ExtractLabel()
    {
        var element = Document.QuerySelector("#main div.details-inner > p > a");
        return element.Text().Trim();
    }

    public IEnumerable<Track> ExtractTracks()
    {
        var trackElements = Document.QuerySelectorAll("#main div.tracks > ol > li");
        return trackElements.Select(ExtractTrack).ToArray();
    }

    private Track ExtractTrack(IElement element)
    {
        return new Track
        {
            Title = ExtractTrackTitle(element),
            Artists = ExtractTrackArtists(element).ToArray()
        };
    }

    private string ExtractTrackTitle(IElement element)
    {
        return HasMainArtists(element) 
            ? element.ChildNodes[4].Text().Substring(" -".Length).Trim() 
            : element.ChildNodes[2].Text().Trim();
    }

    private bool HasMainArtists(IElement element)
    {
        var allArtists = element.QuerySelectorAll("a[data-tooltip-artist]").Length;
        var guestArtists = element.QuerySelectorAll(".subtext > a[data-tooltip-artist]").Length;

        return allArtists != guestArtists;
    }

    private IEnumerable<string> ExtractTrackArtists(IElement element)
    {
        var artists = new List<string>();

        string albumArtist = ExtractAlbumArtist();
        if (!string.IsNullOrWhiteSpace(albumArtist))
        {
            artists.Add(albumArtist);
        }

        var artistElements = element.QuerySelectorAll("a[data-tooltip-artist]");
        foreach (IElement artistElement in artistElements)
        {
            string artist = artistElement.Text();
            if (!artists.Contains(artist))
            {
                artists.Add(artist);
            }
        }

        return artists;
    }
}
