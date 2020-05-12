
using Mercury.DAL.Data;
using Mercury.DAL.Entities;
using Mercury.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercury.DAL.Repository.Concretes
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
            :base(context)
        {

        }
    }
}
