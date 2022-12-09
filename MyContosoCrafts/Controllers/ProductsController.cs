using Microsoft.AspNetCore.Mvc;
using MyContosoCrafts.Models;
using MyContosoCrafts.Services;

namespace MyContosoCrafts.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProductsController : ControllerBase
{
    public ProductsController(JsonFileProductService productService)
    {
        ProductService = productService;
    }

    public JsonFileProductService ProductService
    {
        get;
        private set;
    }

    [HttpGet]
    public IEnumerable<Product> Get() => ProductService.GetProducts();

    [HttpPatch]
    public ActionResult Patch([FromBody] RatingRequest request)
    {
        ProductService.AddRating(request.ProductId, request.Rating);
        return Ok();
    }

    public class RatingRequest
    {
        public string ProductId { get; set; }
        public int Rating { get; set; }
    }
}
