using System.ComponentModel.DataAnnotations;

namespace CryptoAvenue.Dtos.CoinDtos
{
    public class CoinPutPostDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abbreviation { get; set; }
        [Required]
        public double ValueInEUR { get; set; }
        [Required]
        public double ValueInUSD { get; set; }
    }
}
