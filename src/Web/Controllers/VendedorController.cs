using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        private readonly IVendedorService _service;
        public VendedorController(IVendedorService service)
        {
            _service = service;
        }
        [HttpPost]
        public ActionResult<Vendedor> Add([FromBody] CreateRequest request)
        {
            try
            {
                var nuevoVendedor = _service.Add(request);
                return nuevoVendedor;
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id,[FromBody] UpdateRequest request) 
        {
            try
            {
                _service.Update(id, request);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _service?.Delete(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        public ICollection<Vendedor> GetAll()
        {
            return _service.GetAll();
        }
        [HttpGet("{id}")]
        public ActionResult<Vendedor> GetById([FromRoute]int id) 
        {
            try
            {
                return _service.GetById(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
