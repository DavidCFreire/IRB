using IRB.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRB.API.Models
{
    public class DocumentosTagRepository : IDocumentosTagRepository
    {
        private readonly DocumentosTagDBContext _context;
        public DocumentosTagRepository(DocumentosTagDBContext cxt)
        {
            _context = cxt;
        }

        public IEnumerable<Documentos_Tag> GetAll()
        {
            return _context.DOCUMENTOS_TAG.ToList();
        }

        public void Add(Documentos_Tag item)
        {
            _context.DOCUMENTOS_TAG.Add(item);
            _context.SaveChanges();
        }

        public Documentos_Tag Get(int pk)
        {
            return _context.DOCUMENTOS_TAG.FirstOrDefault(u => u.PK == pk);
        }

        public void Remove(int pk)
        {
            var entity = _context.DOCUMENTOS_TAG.First(u => u.PK == pk);
            _context.DOCUMENTOS_TAG.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Documentos_Tag item)
        {
            DetachLocal(_ => _.PK == item.PK);
            _context.DOCUMENTOS_TAG.Update(item);
            _context.SaveChanges();
        }

        public IEnumerable<Documentos_Tag> GetByVersion(int version)
        {
            return _context.DOCUMENTOS_TAG.Where(x => x.VERSAO > version).ToList();
        }

        public virtual void DetachLocal(Func<Documentos_Tag, bool> predicate)
        {
            var local = _context.Set<Documentos_Tag>().Local.Where(predicate).FirstOrDefault();
            if (local != null)
            {
                _context.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
        }
    }
}
