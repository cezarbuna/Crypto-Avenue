using AutoMapper;
using CryptoAvenue.Application.Commands.WalletCommands;
using CryptoAvenue.Application.Queries.WalletQueries;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Dtos.WalletDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoAvenue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public WalletsController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] WalletPutPostDto newWallet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateWalletCommand
            {
                CoinAmount = newWallet.CoinAmount,
                CoinId = newWallet.CoinId,
                UserId = newWallet.UserId
            };

            var wallet = _mapper.Map<WalletPutPostDto, Wallet>(newWallet);

            var addedWallet = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetWalletById), new { Id = wallet.Id }, addedWallet);
        }

        [HttpPost]
        [Route("deposit-to-user-account/{userId}/{option}")]
        public async Task<IActionResult> DepositToUserAccount(Guid userId, int option, [FromBody] double depositedAmount)
        {
            var command = new DepositToAccountCommand
            {
                UserId = userId,
                Option = option,
                DepositedAmount = depositedAmount
            };

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            var wallet = _mapper.Map<WalletGetDto>(result);
            return Ok(wallet);
        }

        [HttpPost]
        [Route("withdraw-from-user-account/{userId}/{option}")]
        public async Task<IActionResult> WithdrawFromUserAccount(Guid userId, int option, [FromBody] double withdrawnAmount)
        {
            var command = new WithdrawFromAccountCommand
            {
                UserId = userId,
                Option = option,
                WithdrawnAmount = withdrawnAmount
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [Route("convert-coins-in-user-wallets/{userId}/{soldCoinId}/{boughtCoinId}")]
        public async Task<IActionResult> ConvertCoinsInUserWallets(Guid userId, Guid soldCoinId, Guid boughtCoinId, [FromBody] double boughtAmount)
        {
            var command = new ConvertCoinsInUserWalletsCommand
            {
                UserId = userId,
                SoldCoinId = soldCoinId,
                BoughtCoinId = boughtCoinId,
                BoughtAmount = boughtAmount
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [Route("copy-user-portfolio/{copiedId}/{pastedId}/{option}")]
        public async Task<IActionResult> CopyUserPortfolio(Guid copiedId, Guid pastedId, int option, [FromBody] int copiedAmount)
        {
            var command = new CopyUserPortfolioCommand
            {
                CopiedUserId = copiedId,
                PastedUserId = pastedId,
                Option = option,
                Amount = copiedAmount
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        [Route("get-all-wallets")]
        public async Task<IActionResult> GetAllWallets()
        {
            var query = new GetAllWalletsQuery();

            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var foundWallets = _mapper.Map<List<WalletGetDto>>(result);
            return Ok(foundWallets);
        }

        [HttpGet]
        [Route("get-wallet-by-id/{id}")]
        public async Task<IActionResult> GetWalletById(Guid id)
        {
            var query = new GetWalletByIdQuery { WalletId = id };

            var wallet = await _mediator.Send(query);

            if (wallet == null)
                return NotFound();

            var foundWallet = _mapper.Map<WalletGetDto>(wallet);

            return Ok(foundWallet);
        }

        [HttpGet]
        [Route("get-all-wallets-by-user-id/{userId}")]
        public async Task<IActionResult> GetAllWalletsByUserId(Guid userId)
        {
            var query = new GetAllWalletsByUserIdQuery { UserId = userId };

            var wallets = await _mediator.Send(query);

            if (wallets == null)
                return NotFound();

            var foundWallets = _mapper.Map<List<WalletGetDto>>(wallets);
            return Ok(foundWallets);
        }

        [HttpGet]
        [Route("get-total-user-portfolio-value/{userId}/{option}")]
        public async Task<IActionResult> GetTotalPortfolioValue(Guid userId, int option)
        {
            var query = new GetTotalPortfolioValueQuery
            {
                UserId = userId,
                Option = option
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("get-portfolio-percentage/{userId}")]
        public async Task<IActionResult> GetPortfolioPercentage(Guid userId)
        {
            var query = new GetPortfolioPercentageQuery { UserId = userId };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete-wallet/{id}")]
        public async Task<IActionResult> DeleteWallet(Guid id)
        {
            var command = new DeleteWalletCommand { WalletId = id };

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            var deletedWallet = _mapper.Map<WalletGetDto>(result);
            return Ok(deletedWallet);
        }
    }
}
