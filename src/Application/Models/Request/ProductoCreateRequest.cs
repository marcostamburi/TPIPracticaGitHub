using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class ProductoCreateRequest
    {
        public string Name {  get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int price { get; set; }
        public int VendedorId { get; set; }
    }
}
