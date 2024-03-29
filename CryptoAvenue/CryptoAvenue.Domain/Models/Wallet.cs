﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.Models
{
    public class Wallet : BaseEntity
    {
        public double CoinAmount { get; set; }
        public Guid CoinId { get; set; }
        public Coin Coin { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
