using CryptoAvenue.Application.Queries.CoinQueries;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.QueryHandlers.CoinQueryHandlers
{
    public class GetCoinByIdQueryHandler : IRequestHandler<GetCoinByIdQuery, Coin>
    {
        private readonly ICoinRepository repository;

        public GetCoinByIdQueryHandler(ICoinRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Coin> Handle(GetCoinByIdQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(repository.GetCoinById(request.CoinId));
        }
    }
}
