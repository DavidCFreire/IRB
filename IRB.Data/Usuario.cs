using System;
using System.Collections.Generic;
using System.Text;

namespace IRB.Data
{
    public class Usuario
    {
        private int _pk;
        public int PK
        {
            get => _pk;
            set
            {
                _pk = value;
            }
        }

        private string _email;
        public string EMAIL
        {
            get => _email;
            set
            {
                _email = value;
            }
        }

        private string _usuario;
        public string USUARIO
        {
            get => _usuario;
            set
            {
                _usuario = value;
            }
        }

        private string _senha;
        public string SENHA
        {
            get => _senha;
            set
            {
                _senha = value;
            }
        }

        private string _tipo;
        public string TIPO
        {
            get => _tipo;
            set
            {
                _tipo = value;
            }
        }

        private string _fontSize;
        public string FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
            }
        }

        private string _lineHeight;
        public string LineHeight
        {
            get => _lineHeight;
            set
            {
                _lineHeight = value;
            }
        }

        private string _textAlignment;
        public string TextAlignment
        {
            get => _textAlignment;
            set
            {
                _textAlignment = value;
            }
        }

        private string _capituloSalvo;
        public string CapituloSalvo
        {
            get => _capituloSalvo;
            set
            {
                _capituloSalvo = value;
            }
        }
    }
}
