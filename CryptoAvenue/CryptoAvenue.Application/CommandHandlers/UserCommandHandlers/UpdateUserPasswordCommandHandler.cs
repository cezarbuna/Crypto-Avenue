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
    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, User>
    {
        private readonly IUserRepository repository;

        public UpdateUserPasswordCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = repository.GetUserById(request.UserId);

            user.Password = request.NewPassword;

            repository.Update(user);
            repository.SaveChanges();

            return await Task.FromResult(user);
        }
    }
}
