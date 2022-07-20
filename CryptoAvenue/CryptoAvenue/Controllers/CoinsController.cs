using AutoMapper;
using CryptoAvenue.Application.Commands.CoinCommands;
using CryptoAvenue.Application.Queries.CoinQueries;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Dtos.CoinDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoAvenue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinsController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public CoinsController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoin([FromBody] CoinPutPostDto newCoin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateCoinCommand
            {
                Name = newCoin.Name,
                Abbreviation = newCoin.Abbreviation,
                ValueInEUR = newCoin.ValueInEUR,
                ValueInUSD = newCoin.ValueInUSD
            };

            var coin = _mapper.Map<CoinPutPostDto, Coin>(newCoin);

            var addedCoin = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetCoinById), new { Id = coin.Id }, addedCoin);
        }

        [HttpGet]
        [Route("get-coin-by-id/{id}")]
        public async Task<IActionResult> GetCoinById(Guid id)
        {
            var query = new GetCoinByIdQuery {  CoinId = id };

            var coin = await _mediator.Send(query);

            if (coin == null)
                return NotFound();

            var foundCoin = _mapper.Map<CoinGetDto>(coin);

            return Ok(foundCoin);
        }

        [HttpGet]
        [Route("get-all-coins")]
        public async Task<IActionResult> GetAllCoins()
        {
            var query = new GetAllCoinsQuery();

            var coins = await _mediator.Send(query);

            if (coins == null)
                return NotFound();

            var foundCoins = _mapper.Map<List<CoinGetDto>>(coins);

            return Ok(coins);
        }

        [HttpPatch]
        [Route("update-coin/{id}")]
        public async Task<IActionResult> UpdateCoin(Guid id, [FromBody] CoinPutPostDto coin)
        {
            var command = new UpdateCoinCommand
            {
                CoinId = id,
                Name = coin.Name,
                Abbreviation = coin.Abbreviation,
                ValueInEUR = coin.ValueInEUR,
                ValueInUSD = coin.ValueInUSD
            };

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }
    }
}
