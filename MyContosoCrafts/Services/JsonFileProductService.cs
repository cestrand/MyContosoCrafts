using System.Text.Json;
using MyContosoCrafts.Models;

namespace MyContosoCrafts.Services;

public class JsonFileProductService
{

    public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
    {
        WebHostEnvironment = webHostEnvironment;
    }

    public IWebHostEnvironment WebHostEnvironment { get; }

    private string JsonFilePath => Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json");

    public IEnumerable<Product> GetProducts()
    {
        using var jsonFileReader = File.OpenText(JsonFilePath);
        return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
            options: new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
    }

    public void AddRating(string productId, int rating)
    {
        var products = GetProducts();
        var product = products.First(x => (x.Id == productId));
        if (product.Rating is null)
        {
            product.Rating = new[] { rating };
        }
        else
        {
            product.Rating = product.Rating.Append(rating).ToArray();
        }
        SaveProducts(products.ToArray());
    }

    public void SaveProducts(Product[] products)
    {
        byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(products, new JsonSerializerOptions
        {
            WriteIndented = true,
        });
        File.WriteAllBytes(JsonFilePath, bytes);
    } 
}
