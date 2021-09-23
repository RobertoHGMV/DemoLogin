using DemoLogin.Api.Security;
using DemoLogin.Api.ViewModels;
using DemoLogin.Domain.Models;
using DemoLogin.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DemoLogin.Api.Controllers
{
    [Route("api")]
    public class LoginController : Controller
    {
        ITokenService _tokenService;
        IUserRepository _repository;

        public LoginController(ITokenService tokenService, IUserRepository repository)
        {
            _tokenService = tokenService;
            _repository = repository;
        }

        [HttpPost]
        [Route("v1/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel { Success = false, Message = "Modelo inválido", Docs = ModelState });

            try
            {
                var user = await _repository.GetByUserName(model.UserName);

                if (user is null)
                    return NotFound(new { message = "Usuário não encontrado" });

                if (!user.Authenticate(model.UserName, model.Password))
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                var token = _tokenService.GenerateToken(user);

                user.Password = "";

                return Ok(new ResultViewModel { Success = true, Docs = new { user = user, token = token } });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Docs = ex });
            }
        }
    }
}
