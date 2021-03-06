﻿using IRB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRB.API.Models
{
    public interface IDocumentoRepository
    {
        void Add(Documento item);
        void Update(Documento item);
        void Remove(int pk);
        Documento Get(int pk);
        IEnumerable<Documento> GetByVersion(int version);
        IEnumerable<Documento> GetAll();
    }
}
