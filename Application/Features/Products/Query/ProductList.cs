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
            public List<ProductReturnDto> ProductReturnDtos { get; set; }
            public int ProductCount { get; set; }
            public int TotalPages { get; set; }
        }
        public class Query:IRequest<ProductEnvelope>
        {
            public Query(int page, int size, string search)
            {
                Page = page;
                Size = size;
                Search = search;
            }

            public int Page { get; set; } 
            public int Size { get; set; }
            public string Search { get; set; }
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
                var queryable = request.Search == null
                    ? _context.Products.AsQueryable()
                    : _context.Products.Where(x => x.Name.ToLower().Contains(request.Search.ToLower())).AsQueryable();

                var products = await queryable.Skip((request.Page - 1) * request.Page)
                    .Take(request.Size).ToListAsync();;
                var totalPages = (int)Math.Ceiling(queryable.Count() / (float)request.Size);
                return new ProductEnvelope
                {
                    ProductReturnDtos = _mapper.Map<List<Product>, List<ProductReturnDto>>(products),
                    ProductCount = queryable.Count(),
                    TotalPages = totalPages
                };
            }
        }
    }
}