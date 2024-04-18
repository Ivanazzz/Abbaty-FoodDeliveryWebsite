using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.Products
{
    public class GetImageMimeTypeTests : BaseServiceTests
    {
        private IProductService productService => this.ServiceProvider.GetRequiredService<IProductService>();

        public GetImageMimeTypeTests()
        {
        }

        [Theory]
        [InlineData("image.jpg", "image/jpeg")]
        [InlineData("image.jpeg", "image/jpeg")]
        [InlineData("image.png", "image/png")]
        [InlineData("image.gif", "image/gif")]
        [InlineData("image.bmp", "image/bmp")]
        [InlineData("file.txt", "application/octet-stream")]
        [InlineData("file.docx", "application/octet-stream")]
        [InlineData("", "application/octet-stream")]
        public void GetImageMimeType_ValidExtensions_ReturnsExpectedMimeType(string fileName, string expectedMimeType)
        {
            // Arrange
            string imagePath = Path.Combine("TestImages", fileName);

            // Act
            string mimeType = InvokePrivateMethod(productService, "GetImageMimeType", imagePath);

            // Assert
            Assert.Equal(expectedMimeType, mimeType);
        }

        private string InvokePrivateMethod(object obj, string methodName, params object[] parameters)
        {
            var methodInfo = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo == null)
            {
                throw new ArgumentException($"Method '{methodName}' not found.", nameof(methodName));
            }

            return methodInfo.Invoke(obj, parameters) as string;
        }
    }
}
