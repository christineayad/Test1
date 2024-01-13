using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataAccess.Repository.IRepository;
using Test.EntityFramework.Models;

namespace Test.DataAccess.Repository
{
    public class MaritalRepository : IMaritalRepository
    {
        private readonly TestContext _dbContext;
        public MaritalRepository(TestContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<MaritalStatus> GetAllStatus()
        {
           return _dbContext.MaritalStatuses.ToList();
        }
    }
}
