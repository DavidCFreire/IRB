using IRB.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRB.Models
{
    public class DocumentoPesquisaModel : Documento
    {
        public string BEFORE { get; set; }
        public string PESQUISA { get; set; }
        public string AFTER { get; set; }
    }
}
