using System;
using System.Collections.Generic;
using System.Text;

namespace IRB.ViewModels
{
    public class ViewModelLocator
    {
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
    }
}
