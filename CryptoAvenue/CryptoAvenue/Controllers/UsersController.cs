using AutoMapper;
using CryptoAvenue.Application.Commands.UserCommands;
using CryptoAvenue.Application.Queries.UserQueries;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Dtos.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoAvenue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public UsersController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserPutPostDto newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateUserCommand
            {
                Username = newUser.Username,
                Password = newUser.Password,
                Email = newUser.Email,
                Age = newUser.Age,
                SecurityQuestion = newUser.SecurityQuestion,
                SecurityAnswer = newUser.SecurityAnswer
            };

            var user = _mapper.Map<UserPutPostDto, User>(newUser);

            var addedUser = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetUserById), new { Id = user.Id }, addedUser);
        }

        [HttpGet]
        [Route("get-user-by-id/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery { UserId = id };

            var user = await _mediator.Send(query);

            if (user == null)
                return NotFound();

            var foundUser = _mapper.Map<UserGetDto>(user);

            return Ok(foundUser);
        }

        [HttpGet]
        [Route("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUsersQuery();

            var users = await _mediator.Send(query);

            if (users == null)
                return NotFound();

            var foundUsers = _mapper.Map<List<UserGetDto>>(users);

            return Ok(foundUsers);
        }
    }
}
