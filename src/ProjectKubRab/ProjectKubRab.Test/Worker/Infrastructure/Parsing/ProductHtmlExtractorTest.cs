using ProjectKubRab.Test.Application.Models;
using ProjectKubRab.Worker.Application.Abstractions;
using ProjectKubRab.Worker.Infrastructure.Parsing;

namespace ProjectKubRab.Test.Worker.Infrastructure.Parsing
{
    public class ProductHtmlExtractorTest
    {
        private readonly IProductHtmlExtractor _productHtmlExtractor = new ProductHtmlExtractor();

        [Fact]
        public async Task ExtractDesiredProduct_WhenCalledWithValidInput_ReturnsExpectedResult()
        {
            //Arrange
            var htmlContent = ProductHtmlContentMock.Create();
            //Act
            var result = await _productHtmlExtractor.ExtractDesiredProductAsync(htmlContent,"ASRock RX 9060 XT CL 16GB AMD Radeon");
            //Assert
            Assert.NotNull(result);
            Assert.Equal("ASRock RX 9060 XT CL 16GB AMD Radeon", result.Name);
            Assert.Equal("R$2.969,77", result.Price);
        }

    }
}
