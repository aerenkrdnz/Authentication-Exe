using AuthenticationExe.Data.Context;
using AuthenticationExe.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationExe.Data.Repositories
{
    public class UserRepository: IUserRepository
        
    {        
        private readonly AuthenticationExeContext _context;
        
        public UserRepository(AuthenticationExeContext context)
        {
            _context = context;
            
        }
        public void AddUser(UserEntity entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public UserEntity Get(Expression<Func<UserEntity, bool>> predicate)
        {
            return _context.Users.FirstOrDefault(predicate);
        }

        public IQueryable<UserEntity> GetAll(Expression<Func<UserEntity, bool>> predicate = null)
        {
            return predicate is not null ? _context.Users.Where(predicate) : _context.Users;
        }
    }
}
