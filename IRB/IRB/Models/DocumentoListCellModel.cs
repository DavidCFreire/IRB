using IRB.Data;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace IRB.Models
{
    public class DocumentoListCellModel : Documento, INotifyPropertyChanged
    {
        public FontAttributes ATTRIBUTES { get; set; }
        public double LINE_HEIGHT { get; set; }
        public int SUB_TITLE_SIZE { get; set; }
        public int TEXT_SIZE { get; set; }
        public int REFERENCIA_SIZE { get; set; }
        public TextAlignment ALIGNMENT { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class DocumentoListCellGrouped : List<DocumentoListCellModel>
    {
        public string PARTE { get; set; }
        public DocumentoListCellGrouped(string parte, List<DocumentoListCellModel> list) : base(list)
        {
            PARTE = parte;
        }
    }
}
