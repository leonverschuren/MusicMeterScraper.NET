using System.Runtime.CompilerServices;

namespace MusicMeterScraper.Tests;

[TestClass]
public class MusicMeterAlbumScraperTests : VerifyBase
{
    private MusicMeterAlbumScraper _sut;

    [ModuleInitializer]
    public static void Init()
    {
        VerifierSettings.DontScrubDateTimes();
    }

    [TestInitialize]
    public void Initialize()
    {
        _sut = new MusicMeterAlbumScraper();
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(13039)]
    [DataRow(757630)]
    [DataRow(233022)]
    public async Task ScrapeAlbumById_ReturnsAllScrapedData(int albumId)
    {
        // Arrange

        // Act
        var result = await _sut.ScrapeAlbumByIdAsync(albumId);

        // Assert
        await Verify(result).UseParameters(albumId);
    }

    [TestMethod]
    public async Task ScrapeAlbumById_AlbumTitleWithParentheses_CorrectYear()
    {
        // Arrange
        var albumId = 30643;

        // Act
        var result = await _sut.ScrapeAlbumByIdAsync(albumId);

        // Assert
        Assert.AreEqual(1997, result.Year);
    }

    [TestMethod]
    public async Task ScrapeAlbumById_ReleaseDateWithWhiteSpace_CorrectReleaseDate()
    {
        // Arrange
        var albumId = 839125;

        // Act
        var result = await _sut.ScrapeAlbumByIdAsync(albumId);

        // Assert
        Assert.AreEqual(new DateTime(2022, 3, 4), result.ReleaseDate);
    }
}
