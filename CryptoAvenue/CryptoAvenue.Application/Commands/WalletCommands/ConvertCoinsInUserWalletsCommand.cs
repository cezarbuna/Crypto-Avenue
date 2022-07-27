using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Commands.WalletCommands
{
    public class ConvertCoinsInUserWalletsCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid SoldCoinId { get; set; }
        public Guid BoughtCoinId { get; set; }
        public double BoughtAmount { get; set; }
    }
}
