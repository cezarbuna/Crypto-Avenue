using System.ComponentModel.DataAnnotations;

namespace CryptoAvenue.Dtos.CoinDtos
{
    public class CoinPutPostDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        public string Abbreviation { get; set; }

        [Required]
        public double ValueInEUR { get; set; }

        [Required]
        public double ValueInUSD { get; set; }
    }
}
