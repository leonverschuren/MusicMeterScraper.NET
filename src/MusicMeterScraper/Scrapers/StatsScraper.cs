using System.Globalization;
using AngleSharp.Dom;

namespace MusicMeterScraper.Scrapers;

internal class StatsScraper
{
    private readonly IDocument _document;

    public StatsScraper(IDocument document)
    {
        _document = document;
    }

    public DateTime? ExtractReleaseDate()
    {
        var paragraphs = _document.QuerySelectorAll("div[data-template=album-stats] > p");
        var element = paragraphs.FirstOrDefault(p => p.Text().StartsWith("releasedatum"));
        if (element == null)
        {
            return null;
        }

        var releaseDateValue = element.Text().Substring("releasedatum: ".Length).Trim();

        return DateTime.ParseExact(releaseDateValue, "d MMMM yyyy", new CultureInfo("nl-NL"));
    }
}
