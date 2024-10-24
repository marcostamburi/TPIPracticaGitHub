using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompradorController : ControllerBase
    {
        private readonly ICompradorService _service;
        public CompradorController(ICompradorService compradorService)
        {
            _service = compradorService;
        }
        [HttpPost]
        [Authorize(Policy = "Sysadmin")]
        public ActionResult<Comprador> Add([FromBody]CreateRequest request) 
        {
            try
            {
                var agregar = _service.Add(request); ;
                return agregar;
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Sysadmin")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            } 
        }


        [HttpPut("{id}")]
        [Authorize(Policy = "Sysadmin")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateRequest request) 
        {
            try
            {
               _service.Update(id, request);
                return Ok();
            }
            catch (NotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public ICollection<Comprador> GetAll() 
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Comprador> GetById([FromRoute]int id) 
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
