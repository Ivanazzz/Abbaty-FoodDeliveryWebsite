using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.UnitTests.Products
{
    public class GetImageBytesTests : BaseServiceTests
    {
        private IProductService productService => this.ServiceProvider.GetRequiredService<IProductService>();

        public GetImageBytesTests()
        {
        }

        [Fact]
        public async Task GetImageBytes_ValidImagePath_ReturnsImageBytes()
        {
            // Arrange
            string rootDirectory = Directory
                .GetParent(AppDomain.CurrentDomain.BaseDirectory)
                .Parent
                .Parent
                .Parent
                .Parent
                .Parent
                .FullName;
            string imagePath = Path.Combine(rootDirectory, @"FoodDeliveryWebsite\FoodDeliveryWebsite\wwwroot\ProductImages\missing.png");

            // Act
            byte[] imageBytes = InvokePrivateMethod(productService, "GetImageBytes", imagePath);

            // Assert
            Assert.NotNull(imageBytes);
            Assert.NotEmpty(imageBytes);
        }

        [Fact]
        public void GetImageBytes_InvalidImagePath_ThrowsFileNotFoundException()
        {
            // Arrange
            string invalidImagePath = "invalid_image_path.jpg";

            // Act & Assert
            Assert.Throws<TargetInvocationException>(() => InvokePrivateMethod(productService, "GetImageBytes", invalidImagePath));
            // TargetInvocationException indicates that an exception was thrown by the method being invoked. It's a wrapper around the actual exception thrown by the method.
        }

        private byte[] InvokePrivateMethod(object obj, string methodName, params object[] parameters)
        {
            var methodInfo = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo == null)
            {
                throw new ArgumentException($"Method '{methodName}' not found.", nameof(methodName));
            }

            return methodInfo.Invoke(obj, parameters) as byte[];
        }
    }
}
