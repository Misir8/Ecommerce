using System.Collections.Generic;
using System.Threading.Tasks;
using API.Helpers;
using Application.Features.Brands.Query;
using Application.Features.Products.Dto;
using Application.Features.Products.Query;
using Application.Features.ProductTypes.Query;
using Application.Pagination;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

namespace API.Controllers
{
    public class ProductsController:BaseController
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<PagedData<ProductReturnDto>>> GetAllProductsAsync([FromQuery] ShopParams @params)
        {
            return await Mediator.Send(new ProductList.Query(@params.Page, @params.Size,
                @params.Search, @params.BrandId, @params.TypeId, @params.Sort));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReturnDto>> GetProductByIdAsync(int id)
        {
            return await Mediator.Send(new ProductDetails.Query{Id = id});
        }

        [HttpGet("brands")]
        public async Task<IEnumerable<ProductBrand>> GetAllBrandsAsync()
        {
            return await Mediator.Send(new BrandList.Query());
        }

        [HttpGet("types")]
        public async Task<IEnumerable<ProductType>> GetAllProductTypesAsync()
        {
            return await Mediator.Send(new ProductTypeList.Query());
        }
    }
}
