using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test.EntityFramework.Models
{
    [Table("MaritalStatus")]
    public partial class MaritalStatus
    {
        public MaritalStatus()
        {
            Clients = new HashSet<Client>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Status { get; set; } 

        [InverseProperty("Status")]
        public virtual ICollection<Client> Clients { get; set; }
    }
}
