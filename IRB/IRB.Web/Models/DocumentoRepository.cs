using IRB.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRB.Web.Models
{
    public class DocumentoRepository : IDocumentoRepository
    {
        private readonly DocumentosDBContext _context;
        public DocumentoRepository(DocumentosDBContext cxt)
        {
            _context = cxt;
        }

        public IEnumerable<Documento> GetAll()
        {
            return _context.DOCUMENTOS.ToList();
        }

        public void Add(Documento item)
        {
            _context.DOCUMENTOS.Add(item);
            _context.SaveChanges();
        }

        public Documento Get(int pk)
        {
            return _context.DOCUMENTOS.FirstOrDefault(u => u.PK == pk);
        }

        public void Remove(int pk)
        {
            var entity = _context.DOCUMENTOS.First(u => u.PK == pk);
            _context.DOCUMENTOS.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Documento item)
        {
            _context.DOCUMENTOS.Update(item);
            _context.SaveChanges();
        }
    }
}
