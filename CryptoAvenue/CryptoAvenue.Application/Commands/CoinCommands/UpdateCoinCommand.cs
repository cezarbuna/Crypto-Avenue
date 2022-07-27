using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Commands.CoinCommands
{
    public class UpdateCoinCommand : IRequest<Coin>
    {
        public Guid CoinId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public double ValueInEUR { get; set; }
        public double ValueInUSD { get; set; }
    }
}
