using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Web.Controller
{
    [Route("api/authentication")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAutenticacionService _autenticacionService;

        public AutenticacionController(IAutenticacionService autenticacionService)
        {
            _autenticacionService = autenticacionService; //Inyectamos el servicio de autenticación
        }

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <remarks>
        /// Returns a JWT token for the user logged in, with a role claim matching the userType passed in the body.
        /// UserType value must be "Comprador", "Vendedor" or "Sysadmin", case sensitive.
        /// </remarks>
        [HttpPost("authenticate")]
        public ActionResult<string> Autenticar(AutenticacionRequest authenticationRequest)
        {
            try
            {

                string token = _autenticacionService.Autenticar(authenticationRequest);

                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
