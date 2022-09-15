
using AutoMapper;
using SkiNetBackend.Dtos;
using SkiNetBackend.Entities;

namespace SkiNetBackend.Helpers;
public class ProductUrlResolver : IValueResolver<Product, ProductsToReturnDto, string>
{
    private readonly IConfiguration _config;
    public ProductUrlResolver(IConfiguration config)
    {
        _config = config;
    }

    public string Resolve(Product source, ProductsToReturnDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {
            return _config["ApiUrl"] + source.PictureUrl;
        }
        return null;
    }
}
