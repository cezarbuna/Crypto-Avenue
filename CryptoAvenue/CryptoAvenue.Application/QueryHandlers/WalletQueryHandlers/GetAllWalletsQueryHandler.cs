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
    public class GetAllWalletsQueryHandler : IRequestHandler<GetAllWalletsQuery, List<Wallet>>
    {
        private readonly IWalletRepository repository;

        public GetAllWalletsQueryHandler(IWalletRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Wallet>> Handle(GetAllWalletsQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(repository.GetAllWalletsBy().ToList());
        }
    }
}
