using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test.EntityFramework.Models
{
    [Table("Client")]
    public partial class Client
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [Required]
        public string? FirstName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [Required]
        public string? LastName { get; set; }
        //[Column(TypeName = "date")]
        [Required]
        public DateTime? DateBirth { get; set; }
        [Required]
        public int? Mobile { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [Required]
        public string? Email { get; set; }
        [Unicode(false)]
        [Required]
       
        public string? Iamge { get; set; }
        [Required]
        public int? StatusId { get; set; }

        [ForeignKey("StatusId")]
        [InverseProperty("Clients")]
        public virtual MaritalStatus? Status { get; set; }
    }
}
