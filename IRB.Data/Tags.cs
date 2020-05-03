using System;
using System.Collections.Generic;
using System.Text;

namespace IRB.Data
{
    public class Tags
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

        private string _descricao;
        public string DESCRICAO
        {
            get => _descricao;
            set
            {
                _descricao = value;
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
