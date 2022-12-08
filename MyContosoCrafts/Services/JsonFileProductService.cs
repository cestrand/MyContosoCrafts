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
}
