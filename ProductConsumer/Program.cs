using Ecommerce.Client;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using static Ecommerce.Client.ProductInfo;

namespace ProductConsumer
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var channel = new Channel("192.168.0.5", 5001, ChannelCredentials.Insecure);
            await channel.ConnectAsync(DateTime.UtcNow.AddSeconds(3));
            var client = new ProductInfoClient(channel);

            Console.WriteLine("insert \"get\" or \"add\"");
            var key = Console.ReadLine();
            if (key.ToLower() == "add")
                await AddProduct(client);

            if (key.ToLower() == "get")
                await GetProduct(client);
            
            Console.WriteLine("Press any key to exit...");
        }

        public static async Task AddProduct(ProductInfoClient client)
        {
             var result = await client.AddProductAsync(new Product(), deadline:DateTime.UtcNow.AddSeconds(11));
            Console.WriteLine("########### RESULT ###########");
            Console.WriteLine($"productId: {result.Value}");
        }

        public static async Task GetProduct(ProductInfoClient client)
        {
             var result = await client.GetProductAsync(new ProductID());

            Console.WriteLine("########### RESULT ###########");
            Console.WriteLine($"product Details: {result.Name} | {result.Description}");
        }
    }
}
