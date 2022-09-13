using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiNetBackend.Data;
using SkiNetBackend.Entities;
using SkiNetBackend.Interfaces;

namespace SkiNetBackend.Controllers;


[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _productsRepo;
    public IGenericRepository<ProductBrand> _productBrandRepo;
    public IGenericRepository<ProductType> _productTypeRepo;

    public ProductsController(
        IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo)
    {
        _productTypeRepo = productTypeRepo;
        _productBrandRepo = productBrandRepo;
        _productsRepo = productsRepo;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var products = await _productsRepo.ListallAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return await _productsRepo.GetByIdAsync(id);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _productBrandRepo.ListallAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        return Ok(await _productTypeRepo.ListallAsync());
    }

}
