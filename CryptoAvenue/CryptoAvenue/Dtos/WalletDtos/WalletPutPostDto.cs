using System.ComponentModel.DataAnnotations;

namespace CryptoAvenue.Dtos.WalletDtos
{
    public class WalletPutPostDto
    {
        [Required]
        public double CoinAmount { get; set; }

        [Required]
        public Guid CoinId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
