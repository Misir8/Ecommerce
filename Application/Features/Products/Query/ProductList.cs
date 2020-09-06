﻿using System;
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
    public enum ProductSortState
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc
    }
    public class ProductList
    {
        public class ProductEnvelope
        {
            public List<ProductReturnDto> ProductReturnDtos { get; set; }
            public int ProductCount { get; set; }
            public int TotalPages { get; set; }
        }
        public class Query:IRequest<ProductEnvelope>
        {
            public Query(int page, int size, string search, string brand, ProductSortState sortState)
            {
                Page = page;
                Size = size;
                Search = search;
                Brand = brand;
                SortState = sortState;
            }

            public int Page { get; set; }
            public int Size { get; set; }
            public string Search { get; set; }
            public string Brand { get; set; }
            public ProductSortState SortState { get; set; }
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
                
                var queryable = request.Search == null && request.Brand == null
                    ? _context.Products.Include(x => x.ProductBrand)
                        .Include(x => x.ProductType).AsQueryable()
                    : _context.Products.Include(x => x.ProductBrand)
                        .Include(x => x.ProductType)
                        .Where(x => (request.Search != null ? x.Name.ToLower().Contains(request.Search.ToLower()) : x.Name != null) &&
                                    (request.Brand != null? x.ProductBrand.Name.ToLower().Contains(request.Brand.ToLower()) : x.ProductBrand.Name != null))
                            .AsQueryable();

                var sortQueryable = request.SortState switch
                {
                    ProductSortState.NameDesc => queryable.OrderByDescending(x => x.Name),
                    ProductSortState.PriceAsc => queryable.OrderBy(x => x.Price),
                    ProductSortState.PriceDesc => queryable.OrderByDescending(x => x.Price),
                    _ => queryable.OrderBy(x => x.Name),
                };

                var products = await sortQueryable.Skip((request.Page - 1) * request.Page)
                    .Take(request.Size).ToListAsync();;
                var totalPages = (int)Math.Ceiling(sortQueryable.Count() / (float)request.Size);
                return new ProductEnvelope
                {
                    ProductReturnDtos = _mapper.Map<List<Product>, List<ProductReturnDto>>(products),
                    ProductCount = sortQueryable.Count(),
                    TotalPages = totalPages
                };
            }
        }
    }
}