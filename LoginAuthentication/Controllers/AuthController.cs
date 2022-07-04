using LoginAuthentication.Dtos;
using LoginAuthentication.Helpers;
using LoginAuthentication.Interfaces;
using LoginAuthentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository repository;
        private readonly JwtService jwtService;

        public AuthController(IUserRepository repository, JwtService jwtService)
        {
            this.repository = repository;
            this.jwtService = jwtService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(RegisterDto user)
        {
            return Created("Success", await repository.Create(user));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto emailDto)
        {
            User emailUser = await repository.GetByEmail(emailDto.Email);
            if (emailUser == null) return BadRequest(new { message = "Invalid Credentials" });
            if (!BCrypt.Net.BCrypt.Verify(emailDto.Password, emailUser.Password)) { return BadRequest(new { message = "Invalid Credentials" }); }
            var jwt = jwtService.Generate(emailUser.Id);
            Response.Cookies.Append("jwt",jwt, new CookieOptions 
            {
                HttpOnly = true
            });
            return Ok(new {message = "Success"});
        }

        [HttpGet("User")]
        public async Task<IActionResult> Users()
        {
            try 
            {
                var jwt = Request.Cookies["jwt"];
                var token = jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                User user = await repository.GetByIdUser(userId);
                return Ok(user);
            } catch (Exception) 
            {
                return Unauthorized();
            }
           
        }

        [HttpPost("Logout")]
        public IActionResult LogOut()
        {
             Response.Cookies.Delete("jwt");

            return Ok(new 
            {
                message = "Success"
            });
        }
    }
}
