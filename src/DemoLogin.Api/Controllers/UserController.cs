using DemoLogin.Api.ViewModels;
using DemoLogin.Domain.Models;
using DemoLogin.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoLogin.Api.Controllers
{
    [Route("api")]
    public class UserController : Controller
    {
        IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("v1/[controller]")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<IList<User>>> GetAll()
        {
            try
            {
                var models = await _repository.GetAll();
                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Docs = ex });
            }
        }

        [HttpGet]
        [Route("v1/[controller]/{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            try
            {
                var model = await _repository.GetById(id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Docs = ex });
            }
        }

        [HttpPost]
        [Route("v1/[controller]")]
        [AllowAnonymous]
        //[Authorize(Roles = "manager")]
        public async Task<IActionResult> Add([FromBody] User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel { Success = false, Message = "Modelo inválido", Docs = ModelState });

            try
            {
                await _repository.Add(model);

                return Created($"api/v1/user/{model.Id}", new ResultViewModel { Success = true, Docs = model });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Docs = ex });
            }
        }

        [HttpPut]
        [Route("v1/[controller]/{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Update(string id, [FromBody] User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel { Success = false, Message = "Modelo inválido", Docs = ModelState });

            if (model.Id != id)
                return NotFound(new ResultViewModel { Success = false, Message = $"Registro não encontrada", Docs = ModelState });

            try
            {
                await _repository.Update(model);

                return Ok(new ResultViewModel { Success = true, Docs = model });
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { Success = false, Message = "Esse registro já foi atualizado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Docs = ex });
            }
        }

        [HttpDelete]
        [Route("v1/[controller]/{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var model = await _repository.GetById(id);

                if (model is null)
                    return NotFound(new ResultViewModel { Success = false, Message = $"Registro não encontrada", Docs = ModelState });

                await _repository.Delete(model);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Docs = ex });
            }
        }
    }
}
