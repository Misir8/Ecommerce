using System;
using System.Threading;
using System.Threading.Tasks;
using API;
using API.Controllers;
using Application.Features.Products.Query;
using AutoMapper;
using MediatR;
using Persistence.Data;
using Xunit;

namespace Ecommerce.Tests
{
    public class ProductListHandler: IDisposable
    {
        private DataContext _context;
        private IMapper _mapper;
        private IMediator _mediator;
        private ProductList.Handler _handler;
        private ProductList.Query _query;
        private ProductsController _controller;
        private BaseController _controllerBase;
        public ProductListHandler()
        {
            _context = ServiceExtension.ResolveRequired<DataContext>();
            _mapper = ServiceExtension.ResolveRequired<IMapper>();
            _query = new ProductList.Query(1,10,string.Empty, null, null, string.Empty);
            _handler = new ProductList.Handler(_context, _mapper);
            _mediator = ServiceExtension.ResolveRequired<IMediator>();
        }
        [Fact]
        public async Task Test()
        {
            var result = await _handler.Handle(_query, new CancellationToken());
            Assert.True(result.Data.Count > 0);
        }

        public void Dispose()
        {
        }
    }
}
