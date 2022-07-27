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
    public class GetAllCoinsQueryHandler : IRequestHandler<GetAllCoinsQuery, List<Coin>>
    {
        private readonly ICoinRepository repository;

        public GetAllCoinsQueryHandler(ICoinRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Coin>> Handle(GetAllCoinsQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(repository.FindAll().ToList());
        }
    }
}
