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
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, Wallet>
    {
        private readonly IWalletRepository walletRepository;
        private readonly ICoinRepository coinRepository;
        private readonly IUserRepository userRepository;

        public CreateWalletCommandHandler
            (IWalletRepository walletRepository,
            ICoinRepository coinRepository,
            IUserRepository userRepository)
        {
            this.walletRepository = walletRepository;
            this.coinRepository = coinRepository;
            this.userRepository = userRepository;
        }

        public async Task<Wallet> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = new Wallet
            {
                CoinAmount = request.CoinAmount,
                CoinId = request.CoinId,
                Coin = coinRepository.GetCoinById(request.CoinId),
                UserId = request.UserId,
                User = userRepository.GetUserById(request.UserId)
            };

            walletRepository.Insert(wallet);
            walletRepository.SaveChanges();

            return await Task.FromResult(wallet);
        }
    }
}
