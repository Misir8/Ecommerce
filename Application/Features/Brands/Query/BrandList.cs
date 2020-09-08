using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Features.Brands.Query
{
    public class BrandList
    {
        public class Query:IRequest<List<ProductBrand>>
        {

        }

        public class Handler :IRequestHandler<Query, List<ProductBrand>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<ProductBrand>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.ProductBrands.ToListAsync();
            }
        }
    }
}
