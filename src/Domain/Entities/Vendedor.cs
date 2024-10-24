using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Vendedor:Usuario
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
        public new Userrole Userrole { get; set; } = Userrole.Vendedor;
    }
}
