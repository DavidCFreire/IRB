using System;
using System.Collections.Generic;
using System.Text;

namespace IRB.Data
{
    public class Documento
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

        private string _tipo;
        public string TIPO
        {
            get => _tipo;
            set
            {
                _tipo = value;
            }
        }

        private int _numero;
        public int NUMERO
        {
            get => _numero;
            set
            {
                _numero = value;
            }
        }

        private string _title;
        public string TITLE
        {
            get => _title;
            set
            {
                _title = value;
            }
        }

        private string _sub_title;
        public string SUB_TITLE
        {
            get => _sub_title;
            set
            {
                _sub_title = value;
            }
        }

        private string _text;
        public string TEXT
        {
            get => _text;
            set
            {
                _text = value;
            }
        }

        private string _referencia;
        public string REFERENCIA
        {
            get => _referencia;
            set
            {
                _referencia = value;
            }
        }

        private string _parte;
        public string PARTE
        {
            get => _parte;
            set
            {
                _parte = value;
            }
        }

    }
}
