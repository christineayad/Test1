using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataAccess.Repository.IRepository;
using Test.EntityFramework.Models;

using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Mvc;

namespace Test.DataAccess.Repository
{
    public class ClientDataAccess : IClientDataAccess
    {
        private readonly TestContext _dbContext;
        [BindProperty]
        public string searchterm { get; set; }

        public ClientDataAccess(TestContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<ClientStatusVM> GetAllClient()
        {
      var clients = _dbContext.Clients .FromSqlRaw($"EXECUTE GetAllClients") .ToList();

      var clientstatus = _dbContext.Clients
            .Include(c => c.Status)
             .Select(m => new ClientStatusVM
             {
                 Id = m.Id,
               FirstName = m.FirstName,
               LastName = m.LastName,
                 DateBirth = m.DateBirth,
                 Mobile = m.Mobile,
                 Email = m.Email,
                 Iamge = m.Iamge,
                 Status = m.Status.Status })
                .ToList();
            return clientstatus;
        }

        public void Add(Client client)
        {
            _dbContext.Clients.Add(client);
           

        }


        public void Edit(int id, Client obj)
        {
            Client oldclient = GetById(id);
            if (oldclient != null)
            {
                oldclient.FirstName = obj.FirstName;
                oldclient.LastName = obj.LastName;
                oldclient.Email = obj.Email;
                oldclient.Mobile = obj.Mobile;
                oldclient.DateBirth = obj.DateBirth;

                oldclient.StatusId = obj.StatusId;

                oldclient.Iamge = obj.Iamge;
               
                
            }
        }

        public void Remove(Client client)
        {
                _dbContext.Clients.Remove(client);
          
        }
        public void save()
        {
            _dbContext.SaveChanges();
        }

        //public List<ClientStatusVM> SearchClientByName (int mobile)
        //{


        //    return _dbContext.Clients .Select(m => new ClientStatusVM
        //    {
        //        Mobile = m.Mobile
        //    })
        //        .Where(u => u.Mobile == mobile).ToList();
        //}

        public List<ClientStatusVM> SearchClientByName(string searchterm)
        {


            return _dbContext.Clients.Select(m => new ClientStatusVM
            {
                Id=m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                DateBirth = m.DateBirth,
                Mobile = m.Mobile,
                Email = m.Email,
                Iamge = m.Iamge,
                Status = m.Status.Status
            
            }).Where(s => s.LastName.Contains(searchterm)
                                       || s.FirstName.Contains(searchterm)).ToList();
            

        }


        public Client GetById(int id)
        {
            return _dbContext.Clients.FirstOrDefault(x => x.Id == id);
        }


    }
}
