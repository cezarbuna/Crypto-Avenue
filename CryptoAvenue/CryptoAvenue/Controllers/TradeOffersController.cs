using AutoMapper;
using CryptoAvenue.Application.Commands.TradeOfferCommands;
using CryptoAvenue.Application.Queries.TradeOfferQueries;
using CryptoAvenue.Domain.Models;
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

        [HttpPost]
        public async Task<IActionResult> CreateTradeOffer([FromBody] TradeOfferPutPostDto newTradeOffer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateTradeOfferCommand
            {
                SenderId = newTradeOffer.SenderId,
                RecipientId = newTradeOffer.RecipientId,
                SentCoinId = newTradeOffer.SentCoinId,
                ReceivedCoinId = newTradeOffer.ReceivedCoinId,
                ReceivedCoinAmount = newTradeOffer.ReceivedCoinAmount
            };

            var tradeOffer = _mapper.Map<TradeOfferPutPostDto, TradeOffer>(newTradeOffer);

            var addedTradeOffer = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetTradeOfferById), new { Id = tradeOffer.Id }, addedTradeOffer);
        }

        [HttpGet]
        [Route("get-all-trade-offers")]
        public async Task<IActionResult> GetAllTradeOffers()
        {
            var query = new GetAllTradeOffersQuery();

            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var foundTradeOffers = _mapper.Map<List<TradeOfferGetDto>>(result);

            return Ok(foundTradeOffers);
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

        [HttpGet]
        [Route("get-all-trade-offers-by-sender-id/{senderId}")]
        public async Task<IActionResult> GetAllTradeOffersBySenderId(Guid senderId)
        {
            var query = new GetAllTradeOffersBySenderIdQuery { SenderId = senderId };

            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var foundTradeOffers = _mapper.Map<List<TradeOfferGetDto>>(result);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-all-trade-offers-by-recipient-id/{recipientId}")]
        public async Task<IActionResult> GetAllTradeOffersByRecipientId(Guid recipientId)
        {
            var query = new GetAllTradeOffersByRecipientIdQuery { RecipientId = recipientId };

            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var foundTradeOffers = _mapper.Map<List<TradeOfferGetDto>>(result);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete-trade-offer/{id}")]
        public async Task<IActionResult> DeleteTradeOffer(Guid id)
        {
            var command = new DeleteTradeOfferCommand { TradeOfferId = id };

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            var deletedTradeOffer = _mapper.Map<TradeOfferGetDto>(result);
            return Ok(result);
        }
    }
}
