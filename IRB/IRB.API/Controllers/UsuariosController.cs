using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IRB.API.Models;
using IRB.Data;
using Microsoft.AspNetCore.Mvc;

namespace IRB.API.Controllers
{
    [Route("[controller]/[action]")]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository repo;
        public UsuariosController(IUsuarioRepository _repo)
        {
            repo = _repo;
        }

        [HttpGet]
        public IEnumerable<Usuario> GetAll()
        {
            return repo.GetAll();
        }

        [HttpGet("{pk}", Name = "GetByPK")]
        public IActionResult GetByPK(int pk)
        {
            var item = repo.Get(pk);
            if (item == null)
                return NotFound();

            return new ObjectResult(item);
        }

        [HttpGet("{username}/{password}")]
        public IActionResult GetByUsernamePassword(string username, string password)
        {
            var item = repo.GetByUserPassword(username, password);
            if (item == null)
                return NotFound();

            return new ObjectResult(item);
        }
        [HttpGet("{username}")]
        public IActionResult GetByUsername(string username)
        {
            var list = repo.GetAll();
            var item = list.Where(x => x.USUARIO == username).FirstOrDefault();
            if (item == null)
                return NotFound();

            return new ObjectResult(item);
        }
        [HttpGet("{email}")]
        public IActionResult GetByEmail(string email)
        {
            var list = repo.GetAll();
            var item = list.Where(x => x.EMAIL == email).FirstOrDefault();
            if (item == null)
                return NotFound();

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Usuario item)
        {
            if (item == null)
                return BadRequest();

            repo.Add(item);
            return CreatedAtAction("GetByPK", new { pk = item.PK }, item);
        }

        [HttpPut("{pk}")]
        public IActionResult Update(int pk, [FromBody] Usuario item)
        {
            if (item == null || item.PK != pk)
                BadRequest();

            repo.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{pk}")]
        public IActionResult Delete(int pk)
        {
            var usuario = repo.Get(pk);
            if (usuario == null)
                NotFound();

            repo.Remove(pk);
            return new NoContentResult();
        }
    }
}