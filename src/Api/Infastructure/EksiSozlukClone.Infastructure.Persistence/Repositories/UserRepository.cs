using EksiSozlukClone.Api.Application.Interfaces.Repositories;
using EksiSozlukClone.Api.Domain.Models;
using EksiSozlukClone.Infastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiSozlukClone.Infastructure.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(EksiSozlukCloneDbContext dbContext) : base(dbContext)
    {

    }
}
