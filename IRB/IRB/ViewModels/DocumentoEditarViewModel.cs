using IRB.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IRB.ViewModels
{
    public class DocumentoEditarViewModel : BaseViewModel
    {
        private DocumentosViewModel DocumentoVm = App.vmLocator.Documentos;
        public DocumentoEditarViewModel()
        {
        }
        private ObservableCollection<Documento> _listLinhas = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListLinhas
        {
            get { return _listLinhas ?? (_listLinhas = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listLinhas, value); }
        }
        private IEnumerable<Documento> _listAllDocumentos;

        public async void Load()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (ModoOffline)
                    _listAllDocumentos = DbHelper.GetAllDocumentos();
                else
                    _listAllDocumentos = await API.GetAllDocumentos();

                Capitulo = new Documento()
                {
                    PK = DocumentoVm._capituloSelected.PK,
                    PARTE = DocumentoVm._capituloSelected.PARTE,
                    NUMERO = DocumentoVm._capituloSelected.NUMERO,
                    REFERENCIA = DocumentoVm._capituloSelected.REFERENCIA,
                    SUB_TITLE = DocumentoVm._capituloSelected.SUB_TITLE,
                    TEXT = DocumentoVm._capituloSelected.TEXT,
                    TIPO = DocumentoVm._capituloSelected.TIPO,
                    TITLE = DocumentoVm._capituloSelected.TITLE,
                    VERSAO = DocumentoVm._capituloSelected.VERSAO
                };
                TITLE = Capitulo.TITLE;
                PARTE = Capitulo.PARTE;
                ListLinhas = new ObservableCollection<Documento>(DocumentoVm.ListLinhas);
            }
            catch
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        private Documento Capitulo;

        private string _titleSelected;
        public string TITLE
        {
            get => _titleSelected;
            set => SetProperty(ref _titleSelected, value);
        }
        private string _parteSelected;
        public string PARTE
        {
            get => _parteSelected;
            set => SetProperty(ref _parteSelected, value);
        }
        public ICommand SalvarCommand
        {
            get
            {
                return new Command(async() =>
                {
                    if (IsBusy)
                        return;

                    IsBusy = true;

                    if(await _messageService.Pergunta("Salvar", "Deseja atualizar este documento?", "Atualizar", "Cancelar"))
                    {
                        if (ModoOffline)
                        {
                            foreach(var item in ListLinhas)
                            {
                                Documento doc = new Documento()
                                {
                                    PK = item.PK,
                                    PARTE = PARTE != null? PARTE : "",
                                    TITLE = TITLE != null? TITLE : "",
                                    VERSAO = 0,
                                    NUMERO = item.NUMERO,
                                    REFERENCIA = item.REFERENCIA != null ? item.REFERENCIA : "",
                                    SUB_TITLE = item.SUB_TITLE != null ? item.SUB_TITLE : "",
                                    TEXT = item.TEXT != null ? item.TEXT : "",
                                    TIPO = item.TIPO != null ? item.TIPO : "",
                                    TITLE_NUMERO = item.TITLE_NUMERO != null ? item.TITLE_NUMERO : ""
                                };
                                DbHelper.UpsertDocumento(doc);
                            }
                            try
                            {
                                SyncDocumentos(true);
                                IsBusy = false;
                                App.vmLocator.Documentos.SelecionarDocumento(string.Empty, string.Empty, false, Capitulo.PK);
                            }
                            catch 
                            {
                                IsBusy = false;
                            }
                            finally
                            {
                                Navigate(".");
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        
                    }
                });
            }
        }

    }
}
