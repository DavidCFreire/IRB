using IRB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRB.API.Models
{
    public interface IUsuarioRepository
    {
        void Add(Usuario item);
        void Update(Usuario item);
        void Remove(int pk);
        Usuario Get(int pk);
        Usuario GetByUserPassword(string username, string password);
        IEnumerable<Usuario> GetAll();
    }
}
