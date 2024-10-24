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
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public Producto Add(ProductoCreateRequest request)
        {
            var producto = new Producto();
            producto.price = request.price;
            producto.Name = request.Name;
            producto.description = request.Description;
            producto.VendedorId = request.VendedorId;

            _repository.Add(producto);
            return producto;
        }

        public void Delete(int id, int vendedorId)
        {
            var borrar = _repository.GetById(id) ?? throw new NotFoundException($"No se encotro el id = {id}");
            if (borrar.VendedorId == vendedorId)
                _repository.Delete(borrar);
            else
                throw new NotFoundException($"Este producto no le pertenece");
        }

        public void Update(int id, ProductoUpdateRequest request)
        {
            var updatear = _repository.GetById(id) ?? throw new NotFoundException($"No se encotro el id = {id}");
            if (updatear.VendedorId == request.VendedorId)
            {
                updatear.price = request.price;
                updatear.Name = request.Name;
                updatear.description = request.Description;

                _repository.Update(updatear);
            }
            else
            {
                throw new NotFoundException($"Este producto no le pertenece");
            }
        }

        public List<Producto> GetAll()
        {
            return _repository.GetAll();
        }

        public Producto GetById(int id)
        {
            return _repository.GetById(id) ?? throw new NotFoundException($"No se encotro el id = {id}");
        }

        public List<Producto> GetProductosConVendedorid(int id)
        {
            var lista = _repository.GetAll().Where(p => p.VendedorId == id);
            return lista.ToList();
        }
    }
}
