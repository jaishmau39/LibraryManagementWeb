using Xunit;
using LibraryManagementWeb;
using DataLibrary;
using LibraryServices;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace LibraryManagementWeb.Tests.CataloguePageTests
{
    public class LoadPagesTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private HttpClient httpClient;
        public LoadPagesTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            httpClient = _factory.CreateClient();
        }
        [Fact (Skip = "Moving to Theory")]
        public async void CatalogueIndexLoadTest()
        {
            // Arrange
            var client = _factory.CreateClient();
            //Act
            var response = await client.GetAsync("/Catalogue/Index");
            int code = (int)response.StatusCode;
            //Assert
            Assert.Equal(200, code);
        }
        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Index")]
        [InlineData("/Patron/Index")]
        [InlineData("/Catalogue/Index")]
        [InlineData("/Branch/Index")]

        public async Task LoadAllPagesTest(string URL)
        {
            // Arrange
            var client = _factory.CreateClient();
            //Act
            var response = await client.GetAsync(URL);
            int code = (int)response.StatusCode;
            //Assert
            Assert.Equal(200, code);
        }

        [Theory]
        [InlineData("The Hitchhikers Guide to the Galaxy")]
        [InlineData("The Book of Five Rings")]
        [InlineData("I Am a Cat")]
        [InlineData("Citizen Kane")]
 
        public async Task CheckCatalogueItemsTest(string CatalogueTitle)
        {
            // Arrange
            
            //Act
            var response = await httpClient.GetAsync("/Catalogue/Index");
            var PageItems = await response.Content.ReadAsStringAsync();
            var PageItemsString = PageItems.ToString();
            //Assert
            Assert.Contains(CatalogueTitle, PageItemsString);
        }
    }
}
