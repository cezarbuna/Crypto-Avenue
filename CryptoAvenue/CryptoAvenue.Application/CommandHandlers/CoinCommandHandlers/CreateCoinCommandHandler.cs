using CryptoAvenue.Application.Commands.CoinCommands;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.CommandHandlers.CoinCommandHandlers
{
    public class CreateCoinCommandHandler : IRequestHandler<CreateCoinCommand, Coin>
    {
        private readonly ICoinRepository repository;

        public CreateCoinCommandHandler(ICoinRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Coin> Handle(CreateCoinCommand request, CancellationToken cancellationToken)
        {
            var coin = new Coin
            {
                Name = request.Name,
                Abbreviation = request.Abbreviation,
                ValueInEUR = request.ValueInEUR,
                ValueInUSD = request.ValueInUSD
            };

            repository.Insert(coin);
            repository.SaveChanges();

            return await Task.FromResult(coin);
        }
    }
}
