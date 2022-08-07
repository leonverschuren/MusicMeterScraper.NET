using FluentAssertions.Json;
using Newtonsoft.Json.Linq;

namespace MusicMeterScraper.Tests;

[TestClass]
public class MusicMeterAlbumScraperTests
{
    private MusicMeterAlbumScraper _sut;

    [TestInitialize]
    public void Initialize()
    {
        _sut = new MusicMeterAlbumScraper();
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(13039)]
    [DataRow(757630)]
    public async Task ScrapeAlbumById_ReturnsAllScrapedData(int albumId)
    {
        // Arrange

        // Act
        var result = await _sut.ScrapeAlbumByIdAsync(albumId);

        // Assert
        var actual = JToken.FromObject(result);
        var expected = JToken.Parse(File.ReadAllText($"Expected/{albumId}.json"));

        actual.Should().BeEquivalentTo(expected);
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
