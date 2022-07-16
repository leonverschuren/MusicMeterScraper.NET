namespace MusicMeterScraper;

public sealed class MusicMeterScraperException : Exception
{
    public MusicMeterScraperException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
