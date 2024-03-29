﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.Models
{
    public class Coin : BaseEntity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public double ValueInEUR { get; set; }
        public double ValueInUSD { get; set; }

        #region Nav Properties
        public ICollection<TradeOffer> OffersSent { get; set; }
        public ICollection<TradeOffer> OffersReceived { get; set; }
        #endregion
    }
}
