using System;
using System.Threading;
using System.Threading.Tasks;
using Ecommerce.Server;
using Grpc.Core;
using Grpc.Core.Testing;
using Grpc.Core.Utils;
using ProductServer;
using Xunit;

namespace ProductUnitTests
{
    public class ServerSideTests
    {
        

        [Fact]
        public async Task Test1()
        {
            var handle = new HandleService();

            var id = Guid.NewGuid().ToString();
            var fakeServerCallContext = TestServerCallContext.Create("fooMethod", null, DateTime.UtcNow.AddHours(1), new Metadata(), CancellationToken.None, "127.0.0.1", null, null, (metadata) => TaskUtils.CompletedTask, () => new WriteOptions(), (writeOptions) => { });
            var response = await handle.AddProduct(new Product { Id =  id}, fakeServerCallContext);
            Assert.NotEmpty(response.Value);
        }
    }
}
