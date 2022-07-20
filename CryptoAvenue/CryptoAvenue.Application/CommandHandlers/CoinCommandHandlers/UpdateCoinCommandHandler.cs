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
    public class UpdateCoinCommandHandler : IRequestHandler<UpdateCoinCommand, Coin>
    {
        private readonly ICoinRepository repository;

        public UpdateCoinCommandHandler(ICoinRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Coin> Handle(UpdateCoinCommand request, CancellationToken cancellationToken)
        {
            var coin = repository.GetCoinById(request.CoinId);

            coin.Name = request.Name;
            coin.Abbreviation = request.Abbreviation;
            coin.ValueInEUR = request.ValueInEUR;
            coin.ValueInUSD = request.ValueInUSD;

            repository.Update(coin);
            repository.SaveChanges();

            return await Task.FromResult(coin);
        }
    }
}
