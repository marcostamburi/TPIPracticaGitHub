using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class CompradorService : ICompradorService
    {
        private readonly ICompradorRepository _repository;

        public CompradorService(ICompradorRepository repository)
        {
            _repository = repository;
        }

        public Comprador Add(CreateRequest request)
        {
            var agregarComprador = new Comprador { Name = request.Name, Email = request.Email, Username = request.Username, Password = request.Password, /*Userrole = Userrole.Comprador*/};
            _repository.Add(agregarComprador);
            return agregarComprador;
        }

        public void Delete(int id)
        {
            var borrar = _repository.GetById(id) ?? throw new NotFoundException($"No se encontro el usuario con el id: {id}");
            _repository.Delete(borrar);
        }

        public void Update(int id, UpdateRequest request)
        {
            var updatear = _repository.GetById(id) ?? throw new NotFoundException($"Unable to update id {id}");
            updatear.Email = request.Email;
            updatear.Name = request.Name;
            updatear.Username = request.Username;
            updatear.Password = request.Password;
            _repository.Update(updatear);
        }

        public List<Comprador> GetAll() 
        { 
            return _repository.GetAll(); 
        }

        public Comprador GetById(int id)
        {
            return _repository.GetById(id) ?? throw new NotFoundException($"Unable to find id {id}");
        }
    }
}
