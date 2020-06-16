using Ecommerce;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace ProductServer
{
    public class HandleService: ProductInfo.ProductInfoBase {

        public override Task<ProductID> AddProduct(Product request, ServerCallContext context)
        {
            return Task<ProductID>.Factory.StartNew(() => new ProductID { Value = Guid.NewGuid().ToString() });
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
