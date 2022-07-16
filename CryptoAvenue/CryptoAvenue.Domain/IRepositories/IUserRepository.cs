using CryptoAvenue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Domain.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetUserBy(Expression<Func<User, bool>> predicate);
        User GetUserById(Guid id);
        IEnumerable<User> GetAllUsersBy(Expression<Func<User, bool>> predicate);
    }
}
