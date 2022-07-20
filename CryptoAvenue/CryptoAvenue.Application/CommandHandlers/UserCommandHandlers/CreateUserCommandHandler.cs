using CryptoAvenue.Application.Commands.UserCommands;
using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Application.CommandHandlers.UserCommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository repository;

        public CreateUserCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (repository.Any(x => x.Email == request.Email))
                throw new Exception("Email is already in use");
            else
            {
                var newUser = new User()
                {
                    Email = request.Email,
                    Username = request.Username,
                    Password = request.Password,
                    Age = request.Age,
                    SecurityQuestion = request.SecurityQuestion,
                    SecurityAnswer = request.SecurityAnswer,
                    PrivateProfile = false
                };

                repository.Insert(newUser);
                repository.SaveChanges();

                return await Task.FromResult(newUser);
            }
        }
    }
}
