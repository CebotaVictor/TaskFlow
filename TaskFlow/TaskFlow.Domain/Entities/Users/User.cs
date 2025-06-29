﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities.Users
{
    [Table("users", Schema = "TaskFlow")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserId",Order = 0, TypeName = "smallint")]
        public ushort Id { get; set; }

        [Required]
        [Column(Order = 2)]
        public string Username {get; set;} = string.Empty;

        [Required]
        [Column(Order = 3)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Column(Order = 1)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column(Order = 5)]
        public DateTime DataAdded { get; set; }

        [Column("UserRole", Order = 4, TypeName = "smallint")]
        public Roles Role { get; set; }
    }
}
