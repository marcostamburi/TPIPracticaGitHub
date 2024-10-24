using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICompradorService
    {
        Comprador Add(CreateRequest request);
        void Delete(int id);
        void Update(int id, UpdateRequest compradorUpdateRequest);
        List<Comprador> GetAll();
        Comprador? GetById(int id);
    }
}
