using System;
using System.Collections.Generic;
using System.Text;

namespace IRB.Data
{
    public class Documentos_Tag
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

        private int _fk_tag;
        public int FK_TAG
        {
            get => _fk_tag;
            set
            {
                _fk_tag = value;
            }
        }
        private int _fk_documento;
        public int FK_DOCUMENTO
        {
            get => _fk_documento;
            set
            {
                _fk_documento = value;
            }
        }
        private int _versao;
        public int VERSAO
        {
            get => _versao;
            set
            {
                _versao = value;
            }
        }
    }
}
