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
    public class GetAllWalletsByUserIdQueryHandler : IRequestHandler<GetAllWalletsByUserIdQuery, List<Wallet>>
    {
        private readonly IWalletRepository repository;

        public GetAllWalletsByUserIdQueryHandler(IWalletRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Wallet>> Handle(GetAllWalletsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var wallets = repository.GetAllWalletsBy(x => x.UserId == request.UserId).ToList();

            return await Task.FromResult(wallets);
        }
    }
}
