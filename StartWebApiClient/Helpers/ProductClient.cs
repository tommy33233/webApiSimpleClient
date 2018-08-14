using System;
using System.Net;
using System.Net.Http;

using System.Threading.Tasks;

namespace StartWebApiClient.Helpers
{
    class ProductClient
    {
        static public HttpClient client = new HttpClient();

        public static void ShowProduct(Product product)
        {
            Console.WriteLine($"Name: {product.Name}\tPrice: " +
                $"{product.Price}\tQuantity: {product.Quantity}");
        }

        public static async Task<Uri> CreateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/product/Create", product);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public static async Task<Product> GetProductAsync(string path)
        {
            Product product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }

        public static async Task<Product> UpdateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/product/{product.Id}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<Product>();
            return product;
        }

        public static async Task<HttpStatusCode> DeleteProductAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/product/{id}");
            return response.StatusCode;
        }

    }
}
