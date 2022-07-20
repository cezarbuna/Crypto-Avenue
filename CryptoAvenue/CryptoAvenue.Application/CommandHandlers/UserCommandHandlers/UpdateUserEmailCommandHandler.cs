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
    public class UpdateUserEmailCommandHandler : IRequestHandler<UpdateUserEmailCommand, User>
    {
        private readonly IUserRepository repository;

        public UpdateUserEmailCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
        {
            var user = repository.GetUserById(request.UserId);

            user.Email = request.NewEmail;

            repository.Update(user);
            repository.SaveChanges();

            return await Task.FromResult(user);
        }
    }
}
