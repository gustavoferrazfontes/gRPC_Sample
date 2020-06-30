using Ecommerce;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace ProductServer
{
    public class HandleService: ProductInfo.ProductInfoBase {

        public override async Task<ProductID> AddProduct(Product request, ServerCallContext context)
        {
            await Task.Delay(10000);
            return await Task<ProductID>.Factory.StartNew(() => new ProductID { Value = Guid.NewGuid().ToString() });
        }

        public override Task<Product> GetProduct(ProductID request, ServerCallContext context)
        {
            return Task<Product>.Factory.StartNew(() => new Product
            {
                Description = "theory and practice about grpc protocol",
                Name = "gRPC up and running",
                Id = request.Value
            }) ;
        }

    }
}
