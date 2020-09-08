using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Features.ProductTypes.Query
{
    public class ProductTypeList
    {
        public class Query: IRequest<List<ProductType>>
        {

        }
        public class Handler:IRequestHandler<Query, List<ProductType>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<ProductType>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.ProductTypes.ToListAsync();
            }
        }
    }
}
