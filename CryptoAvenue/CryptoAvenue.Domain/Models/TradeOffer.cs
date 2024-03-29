﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.Models
{
    public class TradeOffer : BaseEntity
    {
        public Guid SenderId { get; set; }
        public User Sender { get; set; }
        public Guid RecipientId { get; set; }
        public User Recipient { get; set; }
        public Guid SentCoinId { get; set; }
        public Coin SentCoin { get; set; }
        public Guid ReceivedCoinId { get; set; }
        public Coin ReceivedCoin { get; set; }
        public double SentCoinAmount { get; set; }
        public double ReceivedCoinAmount { get; set; }
    }
}
