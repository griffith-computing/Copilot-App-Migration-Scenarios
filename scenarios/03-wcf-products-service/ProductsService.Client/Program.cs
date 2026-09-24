using System;
using ProductsService.Client.ServiceReference;

namespace ProductsService.Client
{
    /// <summary>
    /// Thin console client demonstrating the classic WCF consumption
    /// pattern: instantiate the generated ClientBase-derived proxy, call
    /// it like a local interface, and let WCF handle the SOAP request/
    /// response underneath. Compare this to a modern typed HttpClient or
    /// gRPC client call -- the proxy/channel plumbing here is what
    /// disappears in a migration.
    /// </summary>
    internal static class Program
    {
        private static void Main()
        {
            var client = new ProductServiceClient();
            try
            {
                Console.WriteLine("Products before add:");
                foreach (var product in client.GetProducts())
                {
                    Console.WriteLine($"  #{product.Id} {product.Name} ({product.Category}) - {product.Price:C}");
                }

                var created = client.CreateProduct(new ProductDto
                {
                    Name = "Console-Added Product",
                    Category = "Demo",
                    Price = 19.99m,
                    InStock = true,
                    CreatedDate = DateTime.UtcNow
                });
                Console.WriteLine($"\nCreated product #{created.Id}: {created.Name}");

                var fetched = client.GetProduct(created.Id);
                Console.WriteLine($"Fetched back: {fetched.Name} ({fetched.Category})");

                ((System.ServiceModel.ICommunicationObject)client).Close();
            }
            catch
            {
                ((System.ServiceModel.ICommunicationObject)client).Abort();
                throw;
            }

            Console.WriteLine("\nDone. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
