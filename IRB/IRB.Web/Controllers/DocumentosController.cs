using System;
using System.Collections.Generic;
using System.Linq;
using IRB.Web.Models;
using IRB.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace IRB.Controllers
{
    [Route("[controller]")]
    public class DocumentosController : Controller
    {
        private readonly IDocumentoRepository DocumentoRepository;

        public DocumentosController(IDocumentoRepository documentoRepository)
        {
            DocumentoRepository = documentoRepository;
        }

        [HttpGet]
        public IEnumerable<Documento> List()
        {
            return DocumentoRepository.GetAll().ToList();
        }

        [HttpGet("pk", Name ="GetItem")]
        public IActionResult GetItem(int pk)
        {
            Documento item = DocumentoRepository.Get(pk);

            if (item == null)
                return NotFound();

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Documento item)
        {
            DocumentoRepository.Add(item);
            return CreatedAtAction(nameof(GetItem), new { item.PK }, item);
        }

        [HttpPut("{pk}")]
        public ActionResult Edit([FromBody] Documento item)
        {
            try
            {
                DocumentoRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while editing item");
            }
            return NoContent();
        }

        [HttpDelete("{pk}")]
        public ActionResult Delete(int pk)
        {
            DocumentoRepository.Remove(pk);

            //if (item == null)
            //    return NotFound();

            return Ok();
        }
    }
}
