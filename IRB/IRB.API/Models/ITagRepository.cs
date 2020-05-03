using IRB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRB.API.Models
{
    public interface ITagRepository
    {
        void Add(Tags item);
        void Update(Tags item);
        void Remove(int pk);
        Tags Get(int pk);
        IEnumerable<Tags> GetByVersion(int version);
        IEnumerable<Tags> GetAll();
    }
}
