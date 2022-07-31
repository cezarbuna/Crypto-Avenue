using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Commands.WalletCommands
{
    public class CopyUserPortfolioCommand : IRequest
    {
        public Guid CopiedUserId { get; set; }
        public Guid PastedUserId { get; set; }
        public int Amount { get; set; }
        public int Option { get; set; }
    }
}
