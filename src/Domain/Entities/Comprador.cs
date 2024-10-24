using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Comprador:Usuario
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public new Userrole Userrole { get; set; } = Userrole.Comprador;
    }
}
