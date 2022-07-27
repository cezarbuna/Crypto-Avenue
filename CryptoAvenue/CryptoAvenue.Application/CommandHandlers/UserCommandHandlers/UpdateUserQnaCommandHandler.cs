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
    public class UpdateUserQnaCommandHandler : IRequestHandler<UpdateUserQnaCommand, User>
    {
        private readonly IUserRepository repository;

        public UpdateUserQnaCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> Handle(UpdateUserQnaCommand request, CancellationToken cancellationToken)
        {
            var user = repository.GetUserById(request.UserId);

            user.SecurityQuestion = request.NewSecurityQuestion;
            user.SecurityAnswer = request.NewSecurityAnswer;

            repository.Update(user);
            repository.SaveChanges();

            return await Task.FromResult(user);
        }
    }
}
