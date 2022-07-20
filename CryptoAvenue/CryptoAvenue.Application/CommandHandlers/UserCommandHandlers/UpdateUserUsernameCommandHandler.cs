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
    public class UpdateUserUsernameCommandHandler : IRequestHandler<UpdateUserUsernameCommand, User>
    {
        private readonly IUserRepository repository;

        public UpdateUserUsernameCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> Handle(UpdateUserUsernameCommand request, CancellationToken cancellationToken)
        {
            var user = repository.GetUserById(request.UserId);

            user.Username = request.NewUsername;

            repository.Update(user);
            repository.SaveChanges();

            return await Task.FromResult(user);
        }
    }
}
