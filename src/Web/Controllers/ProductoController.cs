using System.Security.Claims;
using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductoController(IProductoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Sysadmin, Vendedor")]
        public ActionResult<Producto> Add([FromBody] ProductoCreateRequest request)
        {
            try
            {
                var vendedorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vendedorIdClaim == null)
                {
                    return Unauthorized("No se pudo encontrar el Id del vendedor.");
                }

                int vendedorId = int.Parse(vendedorIdClaim);

                // Asignar el Id del vendedor al request antes de crear el producto
                request.VendedorId = vendedorId;

                var nuevoProducto = _service.Add(request);
                return nuevoProducto;
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Sysadmin, Vendedor")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {

                var vendedorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vendedorIdClaim == null)
                {
                    return Unauthorized("No se pudo encontrar el Id del vendedor.");
                }

                int vendedorId = int.Parse(vendedorIdClaim);

                _service?.Delete(id, vendedorId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Sysadmin, Vendedor")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProductoUpdateRequest request)
        {
            try
            {
                var vendedorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (vendedorIdClaim == null)
                {
                    return Unauthorized("No se pudo encontrar el Id del vendedor.");
                }

                int vendedorId = int.Parse(vendedorIdClaim);

                // Asignar el Id del vendedor al request antes de crear el producto
                request.VendedorId = vendedorId;

                _service?.Update(id, request);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public ICollection<Producto> GetAll() 
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> GetById([FromRoute]int id)
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

        [HttpGet()]
        [Authorize(Roles = "Sysadmin, Vendedor")]
        public List<Producto> GetProductosConVendedorid()
        {
            var vendedorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int VendedorId = int.Parse(vendedorIdClaim);
            return _service.GetProductosConVendedorid(VendedorId);
        }
    }
}
