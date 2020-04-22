using IRB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRB.API.Models
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuarioDBContext _context;
        public UsuarioRepository(UsuarioDBContext cxt)
        {
            _context = cxt;
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.USUARIOS.ToList();
        }

        public void Add(Usuario item)
        {
            _context.USUARIOS.Add(item);
            _context.SaveChanges();
        }

        public Usuario Get(int pk)
        {
            return _context.USUARIOS.FirstOrDefault(u => u.PK == pk);
        }

        public void Remove(int pk)
        {
            var entity = _context.USUARIOS.First(u => u.PK == pk);
            _context.USUARIOS.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Usuario item)
        {
            _context.USUARIOS.Update(item);
            _context.SaveChanges();
        }

        public Usuario GetByUserPassword(string username, string password)
        {
            return _context.USUARIOS.FirstOrDefault(u => (u.USUARIO == username || u.EMAIL == username) && u.SENHA == password);
        }
    }
}
