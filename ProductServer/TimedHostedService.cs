using Ecommerce;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Hosting;
using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace ProductServer
{

    public class ProductInfoInterceptor : Interceptor
    {
        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            Console.WriteLine($"Host:{context.Host} | Method: {context.Method}");
            var response =  base.UnaryServerHandler(request, context, continuation);
            return response;
        }
        
    }
    public class TimedHostedService : IHostedService
    {
        private Server server;
       
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    server = new Server()
                    {
                        Ports = { new ServerPort("192.168.0.5", 5001, ServerCredentials.Insecure), },
                        Services = { ProductInfo.BindService(new HandleService()).Intercept(new ProductInfoInterceptor())  }
                    };

                    server.Start();
                    Console.WriteLine("Product Server application running");
                    Console.WriteLine($"host: 192.168.0.5");
                    Console.WriteLine($"port:5001");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
            });

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
