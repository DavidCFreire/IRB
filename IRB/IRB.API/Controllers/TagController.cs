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
    public class TagController : Controller
    {
        private readonly ITagRepository repo;
        public TagController(ITagRepository _repo)
        {
            repo = _repo;
        }

        [HttpGet]
        public IEnumerable<Tags> GetAll()
        {
            return repo.GetAll();
        }

        [HttpGet("{pk}", Name = "GetTagByPk")]
        public IActionResult GetByPK(int pk)
        {
            var item = repo.Get(pk);
            if (item == null)
                return NotFound();

            return new ObjectResult(item);
        }

        [HttpGet("{version}")]
        public IEnumerable<Tags> GetByVersion(int version)
        {
            return repo.GetByVersion(version);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Tags item)
        {
            if (item == null)
                return BadRequest();

            repo.Add(item);
            return CreatedAtAction("GetTagByPk", new { pk = item.PK }, item);
        }

        [HttpPut("{pk}")]
        public IActionResult Update(int pk, [FromBody] Tags item)
        {
            if (item == null || item.PK != pk)
                BadRequest();

            var current = repo.GetAll();

            int versao = current.Max(x => x.VERSAO) + 1;

            item.VERSAO = versao;
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