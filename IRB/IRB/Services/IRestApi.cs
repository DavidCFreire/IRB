using IRB.Data;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IRB.Services
{
    public interface IRestApi
    {
        [Get("/documentos/getall")]
        Task<IEnumerable<Documento>> GetAllDocumentos();

        [Get("/documentos/getbypk/{pk}")]
        Task<Documento> GetByPk(int pk);
    }
}
