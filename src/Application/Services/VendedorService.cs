using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorRepository _vendedorRepository;
        public VendedorService(IVendedorRepository vendedorRepository)
        {
            _vendedorRepository = vendedorRepository;
        }

        public Vendedor Add(CreateRequest request)
        {
            var vendedor = new Vendedor();
            vendedor.email = request.Email;
            vendedor.Name = request.Name;

            _vendedorRepository.Add(vendedor);
            return vendedor;
        }
        public void Update(int id, UpdateRequest request) 
        {
            var updatear = _vendedorRepository.GetById(id) ?? throw new NotFoundException($"No se encotro el id = {id}");
            updatear.email = request.Email;
            updatear.Name = request.Name;
            _vendedorRepository.Update(updatear);
        }
        public void Delete(int id)
        {
            var borrar = _vendedorRepository.GetById(id) ?? throw new NotFoundException($"No se encotro el id = {id}");
            _vendedorRepository.Delete(borrar);

        }
        public List<Vendedor> GetAll() 
        { 
           return _vendedorRepository.GetAll();
        }
        public Vendedor GetById(int id) 
        {
            return _vendedorRepository.GetById(id) ?? throw new NotFoundException($"No se encontró el ID ingresado: {id}");
        }
    }
}
