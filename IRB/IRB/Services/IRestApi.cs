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

        [Get("/documentos/getbyversion/{version}")]
        Task<IEnumerable<Documento>> GetDocumentosByVersion(int version);

        [Put("/documentos/update/{pk}")]
        Task UpdateDocumento(int pk, [Body]Documento ob);

        [Get("/documentos/getbypk/{pk}")]
        Task<Documento> GetByPk(int pk);

        [Get("/usuarios/getbyusernamepassword/{username}/{password}")]
        Task<Usuario> GetUsuarioByUsernameSenha(string username, string password);
        [Get("/usuarios/getbyusername/{username}")]
        Task<Usuario> GetUsuarioByUsername(string username);
        [Get("/usuarios/getbyemail/{email}")]
        Task<Usuario> GetUsuarioByEmail(string email);
        [Post("/usuarios/create")]
        Task<Usuario> CreateUsuario([Body]Usuario ob);
    }
}
