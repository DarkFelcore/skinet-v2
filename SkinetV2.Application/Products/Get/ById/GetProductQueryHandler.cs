using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Common.Errors;
using SkinetV2.Domain.Products;

namespace SkinetV2.Application.Products.Get.ById
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ErrorOr<Product>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Product>> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(Guid.Parse(query.ProductId));

            if(product is null)
            {
                return Errors.Product.NotFound;
            }

            return product;
        }
    }
}