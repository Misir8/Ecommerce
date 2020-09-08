using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Products.Dto;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Query
{
    public class ProductList
    {
        public class ProductEnvelope
        {
            public List<ProductReturnDto> Data { get; set; }
            public int ProductCount { get; set; }
            public int TotalPages { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
        }
        public class Query:IRequest<ProductEnvelope>
        {
            public Query(int page, int size, string search, int? brandId, int? typeId, string sort )
            {
                Page = page;
                Size = size;
                Search = search;
                BrandId = brandId;
                TypeId = typeId;
                Sort = sort;

            }

            public int Page { get; set; }
            public int Size { get; set; }
            public string Search { get; set; }
            public int? BrandId { get; set; }
            public int? TypeId { get; set; }
            public string Sort { get; set; }
        }

        public class Handler: IRequestHandler<Query, ProductEnvelope>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<ProductEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {

                var queryable = request.Search == null && request.BrandId == null && request.TypeId == null
                    ? _context.Products.Include(x => x.ProductBrand)
                        .Include(x => x.ProductType).AsQueryable()
                    : _context.Products.Include(x => x.ProductBrand)
                        .Include(x => x.ProductType)
                        .Where(x => (request.Search != null ? x.Name.ToLower().Contains(request.Search.ToLower()) : x != null) &&
                                    (request.BrandId != null? x.ProductBrand.Id == request.BrandId : x.ProductBrand != null) &&
                                    (request.TypeId != null? x.ProductType.Id == request.TypeId: x.ProductType != null))
                            .AsQueryable();

                var sortQueryable = request.Sort switch
                {
                    "nameAsc" => queryable.OrderBy(x => x.Name),
                    "nameDesc" => queryable.OrderByDescending(x => x.Name),
                    "priceAsc" => queryable.OrderBy(x => x.Price),
                    "priceDesc" => queryable.OrderByDescending(x => x.Price),
                    _ => queryable,
                };

                var products = await sortQueryable.Skip((request.Page - 1) * request.Page)
                    .Take(request.Size).ToListAsync();;
                var totalPages = (int)Math.Ceiling(sortQueryable.Count() / (float)request.Size);
                return new ProductEnvelope
                {
                    Data = _mapper.Map<List<Product>, List<ProductReturnDto>>(products),
                    ProductCount = sortQueryable.Count(),
                    TotalPages = totalPages,
                    Page = request.Page,
                    PageSize = request.Size
                };
            }
        }
    }
}
