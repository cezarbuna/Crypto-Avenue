using System.ComponentModel.DataAnnotations;

namespace CryptoAvenue.Dtos.TradeOfferDtos
{
    public class TradeOfferPutPostDto
    {
        [Required]
        public Guid SenderId { get; set; }
        [Required]
        public Guid RecipientId { get; set; }
        [Required]
        public Guid SentCoinId { get; set; }
        [Required]
        public Guid ReceivedCoinId { get; set; }
        [Required]
        public double ReceivedCoinAmount { get; set; }
        
        //public double ReceivedCoinAmount { get; set; }
    }
}
