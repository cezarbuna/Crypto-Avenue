using CryptoAvenue.Domain.Models;

namespace CryptoAvenue.Dtos.WalletDtos
{
    public class WalletGetDto
    {
        public Guid Id { get; set; }
        public double CoinAmount { get; set; }
        public Guid CoinId { get; set; }
        public Coin Coin { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
