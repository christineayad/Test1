using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Test.EntityFramework.Models
{
    public class ClientStatusVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateBirth { get; set; }
        public int? Mobile { get; set; }
        public string Email { get; set; }
        public string Iamge { get; set; }
       
        public string Status { get; set; }
        //[BindProperty]
        //public string searchterm { get; set; }





    }
}
