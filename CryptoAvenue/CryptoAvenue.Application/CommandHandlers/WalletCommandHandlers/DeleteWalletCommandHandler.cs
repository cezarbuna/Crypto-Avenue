using CryptoAvenue.Application.Commands.WalletCommands;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.CommandHandlers.WalletCommandHandlers
{
    public class DeleteWalletCommandHandler : IRequestHandler<DeleteWalletCommand, Wallet>
    {
        private readonly IWalletRepository repository;

        public DeleteWalletCommandHandler(IWalletRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Wallet> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
        {
            var deletedWallet = repository.GetWalletById(request.WalletId);

            repository.Delete(deletedWallet);
            repository.SaveChanges();

            return await Task.FromResult(deletedWallet);
        }
    }
}
