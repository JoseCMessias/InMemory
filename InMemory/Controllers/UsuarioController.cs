using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMemoryDbAPI.Context;
using InMemoryDbAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InMemoryDbAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly BancoDadosContext _banco;
        public UsuarioController(BancoDadosContext banco) => _banco = banco;

        [HttpGet("Obter")]
        public IActionResult Obter()
        {
            try
            {
                return Ok(_banco.usuario.ToList());
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpGet("Obter/{id}")]
        public IActionResult ObterId(int id)
        {
            try
            {
                var usuarioAtual = _banco.usuario.Where(x => x.Id == id).FirstOrDefault();
                if (usuarioAtual is null)
                    return NotFound();
                else
                    return Ok(usuarioAtual);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Usuarios usuarios)
        {
            try
            {
                _banco.usuario.Add(usuarios);
                _banco.SaveChanges();

                return Ok();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Usuarios usuarios)
        {
            try
            {
                var usuarioAtual = _banco.usuario.Where(x => x.Id == id).FirstOrDefault();
                if (usuarioAtual is null)
                    return NotFound();
                else
                {
                    usuarioAtual.Nome = usuarios.Nome;
                    usuarioAtual.Email = usuarios.Email;
                    _banco.Update(usuarioAtual);
                    _banco.SaveChanges();
                }

                return Ok();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var usuarioAtual = _banco.usuario.Where(x => x.Id == id).FirstOrDefault();
                if (usuarioAtual is null)
                    return NotFound();
                else
                {
                    _banco.Remove(usuarioAtual);
                    _banco.SaveChanges();
                }

                return Ok();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }
    }
}