using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.Products
{
    public class GetMissingImageInfo : BaseServiceTests
    {
        private IProductService productService => this.ServiceProvider.GetRequiredService<IProductService>();

        public GetMissingImageInfo()
        {
        }

        [Fact]
        public async Task GetMissingImageInfo_ReturnsImage()
        {
            // Arrange

            // Act
            Image image = InvokePrivateMethod(productService, "GetMissingImageInfo");

            // Assert
            Assert.NotNull(image);
            Assert.NotNull(image.Data);
            Assert.NotNull(image.Name);
            Assert.NotNull(image.MimeType);
        }

        private Image InvokePrivateMethod(object obj, string methodName, params object[] parameters)
        {
            var methodInfo = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo == null)
            {
                throw new ArgumentException($"Method '{methodName}' not found.", nameof(methodName));
            }

            return methodInfo.Invoke(obj, parameters) as Image;
        }
    }
}
