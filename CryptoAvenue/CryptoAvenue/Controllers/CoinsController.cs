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
    }
}
