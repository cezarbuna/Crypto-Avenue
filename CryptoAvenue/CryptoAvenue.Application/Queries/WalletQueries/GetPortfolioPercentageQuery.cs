using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.Queries.WalletQueries
{
    public class GetPortfolioPercentageQuery : IRequest<Dictionary<double, Coin>>
    {
        public Guid UserId { get; set; }
    }
}
