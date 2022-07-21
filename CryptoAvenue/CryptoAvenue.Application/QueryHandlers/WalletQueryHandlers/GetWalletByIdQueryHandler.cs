using CryptoAvenue.Application.Queries.WalletQueries;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.QueryHandlers.WalletQueryHandlers
{
    public class GetWalletByIdQueryHandler : IRequestHandler<GetWalletByIdQuery, Wallet>
    {
        private readonly IWalletRepository repository;

        public GetWalletByIdQueryHandler(IWalletRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Wallet> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(repository.GetWalletById(request.WalletId));
        }
    }
}
