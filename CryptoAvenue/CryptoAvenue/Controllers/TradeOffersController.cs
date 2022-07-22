using AutoMapper;
using CryptoAvenue.Application.Queries.TradeOfferQueries;
using CryptoAvenue.Dtos.TradeOfferDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoAvenue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeOffersController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public TradeOffersController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-trade-offer-by-id/{id}")]
        public async Task<IActionResult> GetTradeOfferById(Guid id)
        {
            var query = new GetTradeOfferByIdQuery { TradeOfferId = id };

            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var foundTradeOffer = _mapper.Map<TradeOfferGetDto>(result);
            return Ok(result);
        }
    }
}
