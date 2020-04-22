using System;
using System.Collections.Generic;
using System.Text;

namespace IRB.ViewModels
{
    public class ViewModelLocator
    {
        private RegistrarViewModel _registrar;
        public RegistrarViewModel Registrar
        {
            get
            {
                if (_registrar == null)
                    _registrar = new RegistrarViewModel();

                return _registrar;
            }
        }
        private InicioViewModel _inicio;
        public InicioViewModel Inicio
        {
            get
            {
                if (_inicio == null)
                    _inicio = new InicioViewModel();

                return _inicio;
            }
        }
        private DocumentosViewModel _documentos;
        public DocumentosViewModel Documentos
        {
            get
            {
                if (_documentos == null)
                    _documentos = new DocumentosViewModel();

                return _documentos;
            }
        }
        private DocumentoEditarViewModel _documentoEditar;
        public DocumentoEditarViewModel DocumentoEditar
        {
            get
            {
                if (_documentoEditar == null)
                    _documentoEditar = new DocumentoEditarViewModel();

                return _documentoEditar;
            }
        }
        private PesquisaViewModel _pequisa;
        public PesquisaViewModel Pesquisa
        {
            get
            {
                if (_pequisa == null)
                    _pequisa = new PesquisaViewModel();

                return _pequisa;
            }
        }
        private PerfilViewModel _perfil;
        public PerfilViewModel Perfil
        {
            get
            {
                if (_perfil == null)
                    _perfil = new PerfilViewModel();

                return _perfil;
            }
        }
        private EntrarViewModel _entrar;
        public EntrarViewModel Entrar
        {
            get
            {
                if (_entrar == null)
                    _entrar = new EntrarViewModel();

                return _entrar;
            }
        }
    }
}
