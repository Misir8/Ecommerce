using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Products.Dto;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Features.Products.Query
{
    public class ProductDetails
    {
        public class Query:IRequest<ProductReturnDto>
        {
            public int Id { get; set; }            
        }
        
        public class Handler:IRequestHandler<Query, ProductReturnDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<ProductReturnDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Include(x => x.ProductBrand)
                    .Include(x => x.ProductType)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);
                if(product == null) throw new RestException(HttpStatusCode.NotFound, new {message = "Product Not Found"});
                return _mapper.Map<ProductReturnDto>(product);
            }
        }
    }
}