using IRB.Data;
using IRB.Models;
using IRB.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IRB.ViewModels
{
    public class PesquisaViewModel : BaseViewModel
    {
        DocumentosViewModel vmDocumento = App.vmLocator.Documentos;
        public PesquisaViewModel()
        {
        }

        public async Task LoadLivros()
        {
            await Task.Delay(20);
            if (vmDocumento._listAllDocumentos == null)
            {
                await vmDocumento.LoadAllDocumentos();
            }
            List<Documento> listLivros = new List<Documento>();
            listLivros.Add(new Documento() { TIPO = "Todos" });
            foreach (var item in vmDocumento._listAllDocumentos.OrderBy(x => x.TIPO))
            {
                var exist = listLivros.Where(x => x.TIPO == item.TIPO).FirstOrDefault();
                if (exist == null)
                {
                    listLivros.Add(item);
                }
            }
            ListDocumentos = new ObservableCollection<Documento>(listLivros);
            DocumentoFilter = ListDocumentos.FirstOrDefault();
        }

        private ObservableCollection<DocumentoPesquisaModel> _listPesquisa = new ObservableCollection<DocumentoPesquisaModel>();
        public ObservableCollection<DocumentoPesquisaModel> ListPesquisa
        {
            get { return _listPesquisa ?? (_listPesquisa = new ObservableCollection<DocumentoPesquisaModel>()); }
            set { SetProperty(ref _listPesquisa, value); }
        }

        private ObservableCollection<Documento> _listDocumentos = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListDocumentos
        {
            get { return _listDocumentos ?? (_listDocumentos = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listDocumentos, value); }
        }

        public ICommand PesquisarCommand
        {
            get
            {
                return new Command( async() =>
                {
                    await Pesquisar();
                });
            }
        }

        private async Task Pesquisar()
        {
            IsBusy = true;

            ListPesquisa.Clear();
            if (!string.IsNullOrEmpty(_pesquisa))
            {
                try
                {
                    List<DocumentoPesquisaModel> list = new List<DocumentoPesquisaModel>();
                    if (vmDocumento._listAllDocumentos == null)
                    {
                        await vmDocumento.LoadAllDocumentos();
                    }
                    //list = vmDocumento._listAllDocumentos.Where(x => (x.PARTE != null)?x.PARTE.ToLower().Contains(_pesquisa.ToLower()):false || (x.REFERENCIA!=null)?x.REFERENCIA.ToLower().Contains(_pesquisa.ToLower()) :false || (x.SUB_TITLE != null)?x.SUB_TITLE.ToLower().Contains(_pesquisa.ToLower()) :false || (x.TEXT != null) ? x.TEXT.ToLower().Contains(_pesquisa.ToLower()) :false || (x.TIPO != null) ? x.TIPO.ToLower().Contains(_pesquisa.ToLower()) :false || (x.TITLE != null) ? x.TITLE.ToLower().Contains(_pesquisa.ToLower()) :false);
                    var list_prefilter = vmDocumento._listAllDocumentos.Where(x => x.TEXT.ToLower().Contains(_pesquisa.ToLower()) || x.TITLE.ToLower().Contains(_pesquisa.ToLower()) || x.TITLE_NUMERO.ToLower().Contains(_pesquisa.ToLower()));
                    if (DocumentoFilter != null && DocumentoFilter.PK > 0 && DocumentoFilter.TIPO != "Todos")
                    {
                        list_prefilter = list_prefilter.Where(x => x.TIPO == DocumentoFilter.TIPO);
                    }
                    foreach (var item in list_prefilter)
                    {
                        var exits = list.Where(x => x.TIPO == item.TIPO && x.TITLE == item.TITLE).FirstOrDefault();
                        if (exits == null)
                        {
                            DocumentoPesquisaModel itemPesquisa = new DocumentoPesquisaModel()
                            {
                                PK = item.PK,
                                NUMERO = item.NUMERO,
                                PARTE = item.PARTE,
                                REFERENCIA = item.REFERENCIA,
                                SUB_TITLE = item.SUB_TITLE,
                                TEXT = item.TEXT,
                                TIPO = item.TIPO,
                                TITLE = item.TITLE,
                                TITLE_NUMERO = item.TITLE_NUMERO,
                                VERSAO = item.VERSAO
                            };
                            itemPesquisa.BEFORE = StringUtils.Before(item.TEXT.ToLower(), _pesquisa.ToLower(), 50);
                            itemPesquisa.PESQUISA = _pesquisa;
                            itemPesquisa.AFTER = StringUtils.After(item.TEXT.ToLower(), _pesquisa.ToLower(), 50);
                            list.Add(itemPesquisa);
                        }
                    }


                    if (list != null && list.Count() > 0)
                    {
                        ListPesquisa = new ObservableCollection<DocumentoPesquisaModel>(list);
                    }
                }
                catch
                {
                    await DownloadDocumentos();
                    if(await _messageService.Pergunta("Pesquisa", "Tivemos um problema com a pesquisa, deseja tentar novamente?", "Tentar Novamente", "Cancelar"))
                    {
                        await Pesquisar();
                    }
                }
            }

            IsBusy = false;
        }


        private Documento _documentoFilter;
        public Documento DocumentoFilter
        {
            get => _documentoFilter;
            set
            {
                SetProperty(ref _documentoFilter, value);
                Pesquisar();
            }
        }

        private DocumentoPesquisaModel _capituloPesquisaSelected;
        public DocumentoPesquisaModel CapituloPesquisaSelected
        {
            get => _capituloPesquisaSelected;
            set
            {
                SetProperty(ref _capituloPesquisaSelected, value);
                OpenDocumento();
            }
        }

        public void OpenDocumento()
        {
            var docPK = vmDocumento._listAllDocumentos.Where(x => x.TIPO == _capituloPesquisaSelected.TIPO && x.NUMERO == _capituloPesquisaSelected.NUMERO).Min(x => x.PK);
            vmDocumento.SelecionarDocumento(string.Empty, string.Empty, false, docPK);
        }

        private string _pesquisa;
        public string Pesquisa
        {
            get => _pesquisa;
            set => SetProperty(ref _pesquisa, value);
        }
    }
}
