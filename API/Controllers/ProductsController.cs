using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Brands.Query;
using Application.Features.Products.Dto;
using Application.Features.Products.Query;
using Application.Features.ProductTypes.Query;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController:BaseController
    {

        [HttpGet]
        public async Task<ActionResult<ProductList.ProductEnvelope>> GetAllProductsAsync(string search , int? brandId,
            int? typeId,int page = 1, int size = 10, ProductSortState sortState = ProductSortState.NameAsc )
        {
            return await Mediator.Send(new ProductList.Query(page, size, search, brandId, typeId, sortState));
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
