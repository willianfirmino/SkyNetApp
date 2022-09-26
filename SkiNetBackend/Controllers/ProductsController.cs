using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkiNetBackend.Dtos;
using SkiNetBackend.Entities;
using SkiNetBackend.Interfaces;
using SkiNetBackend.Specifications;

namespace SkiNetBackend.Controllers;


public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productsRepo;
    public IGenericRepository<ProductBrand> _productBrandRepo;
    public IGenericRepository<ProductType> _productTypeRepo;
    private readonly IMapper _mapper;

    public ProductsController(
        IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
    {
        _mapper = mapper;
        _productTypeRepo = productTypeRepo;
        _productBrandRepo = productBrandRepo;
        _productsRepo = productsRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductsToReturnDto>>> GetProducts(
       ProductSpecParams productParams)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
        var products = await _productsRepo.ListAsync(spec);
        return Ok(_mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductsToReturnDto>>(products));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductsToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await _productsRepo.GetEntityWitSpec(spec);

        if (product == null) return NotFound();

        return _mapper.Map<Product, ProductsToReturnDto>(product);
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
