using CryptoAvenue.Application.Commands.TradeOfferCommands;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.CommandHandlers.TradeOfferCommandHandlers
{
    public class DeleteTradeOfferCommandHandler : IRequestHandler<DeleteTradeOfferCommand, TradeOffer>
    {
        private readonly ITradeOfferRepository repository;

        public DeleteTradeOfferCommandHandler(ITradeOfferRepository repository)
        {
            this.repository = repository;
        }

        public async Task<TradeOffer> Handle(DeleteTradeOfferCommand request, CancellationToken cancellationToken)
        {
            var deletedTradeOffer = repository.GetTradeOfferById(request.TradeOfferId);

            repository.Delete(deletedTradeOffer);
            repository.SaveChanges();

            return await Task.FromResult(deletedTradeOffer);
        }
    }
}
