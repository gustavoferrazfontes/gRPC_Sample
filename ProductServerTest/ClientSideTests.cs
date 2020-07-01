using Ecommerce.Client;
using Grpc.Core;
using Grpc.Core.Testing;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Ecommerce.Client.ProductInfo;

namespace ProductUnitTests
{
    public class ClientSideTests
    {
        [Fact]
        public void Test1()

        {
            var mockClient = new Moq.Mock<ProductInfoClient>();

            var fakeCall = TestCalls.AsyncUnaryCall(Task.FromResult(new ProductID()), Task.FromResult(new Metadata()), () => Status.DefaultSuccess, () => new Metadata(), () => { });

            mockClient.Setup(m => m.AddProductAsync(Moq.It.IsAny<Product>(), null, null, CancellationToken.None)).Returns(fakeCall);

            Assert.Same(fakeCall, mockClient.Object.AddProductAsync(new Product()));
        }

    }  
}
