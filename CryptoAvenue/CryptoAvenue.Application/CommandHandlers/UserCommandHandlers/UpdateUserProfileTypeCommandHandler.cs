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
    public class UpdateUserProfileTypeCommandHandler : IRequestHandler<UpdateUserProfileTypeCommand, User>
    {
        private readonly IUserRepository repository;

        public UpdateUserProfileTypeCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> Handle(UpdateUserProfileTypeCommand request, CancellationToken cancellationToken)
        {
            var user = repository.GetUserById(request.UserId);

            user.PrivateProfile = !user.PrivateProfile;

            repository.Update(user);
            repository.SaveChanges();

            return await Task.FromResult(user);
        }
    }
}
