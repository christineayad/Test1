using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.EntityFramework.Models;

namespace Test.DataAccess.Repository.IRepository
{
    public interface IMaritalRepository
    {
        List<MaritalStatus> GetAllStatus();
        //MaritalStatus GetById(int id);
        //void Add(MaritalStatus client);
        //void Edit(int id, MaritalStatus client);
        //void Delete(int id);
        
    }
}
