using CryptoAvenue.Domain.IRepositories;
using CryptoAvenue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Dal.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(CryptoAvenueDbContext context) : base(context)
        {
        }

        public IEnumerable<User> GetAllUsersBy(Expression<Func<User, bool>> predicate)
        {
            return context.Users.Where(predicate);
        }

        public User GetUserBy(Expression<Func<User, bool>> predicate)
        {
            return context.Users.SingleOrDefault(predicate);
        }

        public User GetUserById(Guid id)
        {
            return context.Users.SingleOrDefault(x => x.Id == id);
        }
    }
}
