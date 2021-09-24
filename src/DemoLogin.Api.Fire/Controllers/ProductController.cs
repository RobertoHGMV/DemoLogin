using DemoLogin.Api.Fire.ViewModels;
using DemoLogin.Domain.Models;
using DemoLogin.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoLogin.Api.Fire.Controllers
{
    [Route("api")]
    [Authorize]
    public class ProductController : Controller
    {
        IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("v1/[controller]")]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

                var models = await _repository.GetAll(userId);

                return Ok(new ResultViewModel { Success = true, Docs = models });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Docs = ex });
            }
        }

        [HttpGet]
        [Route("v1/[controller]/{id:int}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

                var model = await _repository.GetById(id, userId);

                return Ok(new ResultViewModel { Success = true, Docs = model });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Docs = ex });
            }
        }

        [HttpPost]
        [Route("v1/[controller]")]
        public async Task<IActionResult> Add([FromBody] Product model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel { Success = false, Message = "Modelo inválido", Docs = ModelState });

            try
            {
                model.UserId = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

                await _repository.Add(model);

                return Created($"api/v1/product/{model.Id}", new ResultViewModel { Success = true, Docs = model });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Docs = ex });
            }
        }

        [HttpPut]
        [Route("v1/[controller]/{id:int}")]
        public async Task<IActionResult> Update([FromBody] Product model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel { Success = false, Message = "Modelo inválido", Docs = ModelState });

            try
            {
                model.UserId = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

                var product = await _repository.GetById(model.Id, model.UserId);

                if (product is null)
                    return NotFound(new ResultViewModel { Success = false, Message = $"Registro não encontrada", Docs = ModelState });

                product.Description = model.Description;

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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

                var model = await _repository.GetById(id, userId);

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
