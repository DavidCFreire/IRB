using IRB.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRB.API.Models
{
    public class TagsRepository : ITagRepository
    {
        private readonly TagsDBContext _context;
        public TagsRepository(TagsDBContext cxt)
        {
            _context = cxt;
        }

        public IEnumerable<Tags> GetAll()
        {
            return _context.TAGS.ToList();
        }

        public void Add(Tags item)
        {
            _context.TAGS.Add(item);
            _context.SaveChanges();
        }

        public Tags Get(int pk)
        {
            return _context.TAGS.FirstOrDefault(u => u.PK == pk);
        }

        public void Remove(int pk)
        {
            var entity = _context.TAGS.First(u => u.PK == pk);
            _context.TAGS.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Tags item)
        {
            DetachLocal(_ => _.PK == item.PK);
            _context.TAGS.Update(item);
            _context.SaveChanges();
        }

        public IEnumerable<Tags> GetByVersion(int version)
        {
            return _context.TAGS.Where(x => x.VERSAO > version).ToList();
        }

        public virtual void DetachLocal(Func<Tags, bool> predicate)
        {
            var local = _context.Set<Tags>().Local.Where(predicate).FirstOrDefault();
            if (local != null)
            {
                _context.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
        }
    }
}
