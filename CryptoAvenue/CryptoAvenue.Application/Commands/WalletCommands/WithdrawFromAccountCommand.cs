using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Commands.WalletCommands
{
    public class WithdrawFromAccountCommand : IRequest
    {
        public Guid UserId { get; set; }
        public int Option { get; set; } // 0 for EUR and 1 for USD
        public double WithdrawnAmount { get; set; }
    }
}
