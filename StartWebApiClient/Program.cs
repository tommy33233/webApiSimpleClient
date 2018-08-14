using StartWebApiClient.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StartWebApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            
            
            // Update port # in the following line.
            ProductClient.client.BaseAddress = new Uri("http://localhost:55094/");
            ProductClient.client.DefaultRequestHeaders.Accept.Clear();
            ProductClient.client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                Product product = new Product
                {
                    Id = 10,
                    Name = "Gizmo",
                    Price = 100,
                    Quantity = 15
                };

                var url = await ProductClient.CreateProductAsync(product);
                Console.WriteLine($"Created at {url}");

                // Get the product
                product = await ProductClient.GetProductAsync(url.PathAndQuery);
                ProductClient.ShowProduct(product);

                // Update the product
                Console.WriteLine("Updating price...");
                product.Price = 80;
                await ProductClient.UpdateProductAsync(product);

                // Get the updated product
                product = await ProductClient.GetProductAsync(url.PathAndQuery);
                ProductClient.ShowProduct(product);

                // Delete the product
                var statusCode = await ProductClient.DeleteProductAsync(product.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}

