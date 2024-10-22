using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Producto : Usuario
    {
        [Required]
        [StringLength(50)]
        public string description { get; set; } = string.Empty;
        [Required]
        public int price { get; set; }
        [ForeignKey("VendedorId")]
        public Vendedor Vendedor { get; set; }
        public int? VendedorId { get; set; }
    }
}
