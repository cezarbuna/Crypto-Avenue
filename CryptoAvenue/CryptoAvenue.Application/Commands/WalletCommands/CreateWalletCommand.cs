using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Commands.WalletCommands
{
    public class CreateWalletCommand : IRequest<Wallet>
    {
        public double CoinAmount { get; set; }
        public Guid CoinId { get; set; }
        public Guid UserId { get; set; }
    }
}
