using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductoService
    {
        Producto Add(ProductoCreateRequest request);
        void Delete(int id, int vendedorId);
        void Update(int id, ProductoUpdateRequest request);
        List<Producto> GetAll();
        Producto? GetById(int id);
        List<Producto> GetProductosConVendedorid(int id);
    }
}
