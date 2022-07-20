namespace CryptoAvenue.Dtos.CoinDtos
{
    public class CoinGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public double ValueInEUR { get; set; }
        public double ValueInUSD { get; set; }
    }
}
