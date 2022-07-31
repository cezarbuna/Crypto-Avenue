using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Commands.WalletCommands
{
    public class DeleteWalletCommand : IRequest<Wallet>
    {
        public Guid WalletId { get; set; }
    }
}
