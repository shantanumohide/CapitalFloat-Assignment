using Microsoft.AspNetCore.Mvc;
using ProductApi.Services;

namespace ProductApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{

    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try{
            var products = await _productService.GetAll();
            return Ok(products);
        }catch(Exception ex){
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try{
            var product = await _productService.GetById(id);
            return Ok(product);
        }catch(Exception ex){
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product _product){
        try{
            await _productService.Create(_product);
            return Ok("Success");
        }catch(Exception ex){
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Product _product){
        try{
            await _productService.Update(_product);
            return Ok("Success");
        }catch(Exception ex){
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id){
        try{
            await _productService.Delete(id);
            return Ok("Success");
        }catch(Exception ex){
            return NotFound(ex.Message);
        }
    }
}
