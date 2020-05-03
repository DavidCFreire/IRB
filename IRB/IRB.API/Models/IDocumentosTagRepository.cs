using IRB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRB.API.Models
{
    public interface IDocumentosTagRepository
    {
        void Add(Documentos_Tag item);
        void Update(Documentos_Tag item);
        void Remove(int pk);
        Documentos_Tag Get(int pk);
        IEnumerable<Documentos_Tag> GetByVersion(int version);
        IEnumerable<Documentos_Tag> GetAll();
    }
}
