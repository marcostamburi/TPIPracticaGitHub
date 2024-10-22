using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IVendedorService
    {
        Vendedor Add(CreateRequest request);
        void Delete(int id);
        void Update(int id, UpdateRequest vendedorUpdateRequest);
        List<Vendedor> GetAll();
        Vendedor? GetById(int id);
    }
}
