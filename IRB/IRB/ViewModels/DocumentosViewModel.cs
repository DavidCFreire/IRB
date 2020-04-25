using IRB.Data;
using IRB.Models;
using IRB.Utils;
using IRB.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IRB.ViewModels
{
    public class DocumentosViewModel : BaseViewModel
    {
        public DocumentosViewModel()
        {
            //CarregarPagina();
        }
        public string _pageName;
        public string _parteFiltro;
        public bool _linhas;
        public int _pkUp;
        public async Task SelecionarDocumento(string pageName, string parte = "", bool linhas = false, int pkUp = 0)
        {
            _pageName = pageName;
            _parteFiltro = parte;
            _linhas = linhas;
            _pkUp = pkUp;
            if (ListDocumentos != null && ListDocumentos.Count > 0 && pkUp == 0)
            {
                if (string.IsNullOrEmpty(parte))
                {
                    LivroSelected = ListDocumentos.Where(x => x.TIPO == pageName).FirstOrDefault();
                }
                else
                {
                    LivroSelected = ListDocumentos.Where(x => x.TIPO == pageName && x.PARTE.Contains(parte)).FirstOrDefault();
                }
            }
            else
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                await LoadLivros();
            }
        }

        private bool _retornando = false;
        public bool Retornando
        {
            get => _retornando;
            set => SetProperty(ref _retornando, value);
        }

        private async void CarregarPagina()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            //string savedJson = Preferences.Get(Settings.CapituloSalvo, string.Empty);
            //if (!string.IsNullOrEmpty(savedJson))
            //    Retornando = true;
            //else
            //    Retornando = false;
            await LoadLivros();
            //if (!string.IsNullOrEmpty(savedJson))
            //{
            //    saved = JsonConvert.DeserializeObject<DocumentoListCellModel>(savedJson);
            //    LivroSelected = saved;
            //    //var cs = _listDocumentos.Where(i => i.PK == saved.PK).FirstOrDefault();
            //    //CapituloSelected = cs;
            //}
        }
        DocumentoListCellModel saved;

        private IEnumerable<Documento> _listAllDocumentos;

        #region CAPITULOS
        private ObservableCollection<DocumentoListCellGrouped> _listCapitulos = new ObservableCollection<DocumentoListCellGrouped>();
        public ObservableCollection<DocumentoListCellGrouped> ListCapitulos
        {
            get { return _listCapitulos ?? (_listCapitulos = new ObservableCollection<DocumentoListCellGrouped>()); }
            set { SetProperty(ref _listCapitulos, value); }
        }
        private ObservableCollection<DocumentoListCellModel> _listCapitulosNonGrouped = new ObservableCollection<DocumentoListCellModel>();
        public ObservableCollection<DocumentoListCellModel> ListCapitulosNonGrouped
        {
            get { return _listCapitulosNonGrouped ?? (_listCapitulosNonGrouped = new ObservableCollection<DocumentoListCellModel>()); }
            set { SetProperty(ref _listCapitulosNonGrouped, value); }
        }
        private ObservableCollection<DocumentoListCellModel> _listAllCapitulosNonGrouped = new ObservableCollection<DocumentoListCellModel>();
        public ObservableCollection<DocumentoListCellModel> ListAllCapitulosNonGrouped
        {
            get { return _listAllCapitulosNonGrouped ?? (_listAllCapitulosNonGrouped = new ObservableCollection<DocumentoListCellModel>()); }
            set { SetProperty(ref _listAllCapitulosNonGrouped, value); }
        }
        //RefreshCommand
        public ICommand RefreshCapitulosCommand => new Command(async () => await LoadCapitulos());

        async Task LoadCapitulos()
        {
            if(_listAllDocumentos != null)
            {
                ListCapitulos.Clear();
                List<DocumentoListCellGrouped> listCapitulos = new List<DocumentoListCellGrouped>();
                List<DocumentoListCellModel> listCapitulosNonGrouped = new List<DocumentoListCellModel>();
                //IEnumerable<Documento> listFiltered;
                //if (string.IsNullOrEmpty(_parteFiltro))
                //    listFiltered = _listAllDocumentos.Where(x => x.TIPO == _livroSelected.TIPO);
                //else
                //    listFiltered = _listAllDocumentos.Where(x => x.TIPO == _livroSelected.TIPO && x.PARTE.Contains(_parteFiltro));

                //foreach (var i in listFiltered)
                //{
                //    DocumentoListCellModel item = new DocumentoListCellModel()
                //    {
                //        PK = i.PK,
                //        PARTE = i.PARTE,
                //        NUMERO = i.NUMERO,
                //        REFERENCIA = i.REFERENCIA,
                //        SUB_TITLE = i.SUB_TITLE,
                //        TEXT = i.TEXT,
                //        TIPO = i.TIPO,
                //        TITLE = i.TITLE,
                //        ATTRIBUTES = FontAttributes.None,
                //        LINE_HEIGHT = LINE_HEIGHT,
                //        REFERENCIA_SIZE = REFERENCIA_SIZE,
                //        SUB_TITLE_SIZE = SUB_TITLE_SIZE,
                //        TEXT_SIZE = TEXT_SIZE,
                //        ALIGNMENT = ALIGNMENT
                //    };
                //    var exist2 = list.Where(x => x.TITLE == item.TITLE).FirstOrDefault();
                //    if (exist2 == null)
                //    {
                //        list.Add(item);
                //        listCapitulosNonGrouped.Add(item);
                //    }
                //}
                foreach (var g in _listAllDocumentos.Where(x => x.TIPO == _livroSelected.TIPO))
                {
                    DocumentoListCellModel gi = new DocumentoListCellModel()
                    {
                        PK = g.PK,
                        PARTE = g.PARTE,
                        NUMERO = g.NUMERO,
                        REFERENCIA = g.REFERENCIA,
                        SUB_TITLE = g.SUB_TITLE,
                        TEXT = g.TEXT,
                        TIPO = g.TIPO,
                        TITLE = g.TITLE,
                        ATTRIBUTES = FontAttributes.None,
                        LINE_HEIGHT = LINE_HEIGHT,
                        REFERENCIA_SIZE = REFERENCIA_SIZE,
                        SUB_TITLE_SIZE = SUB_TITLE_SIZE,
                        TEXT_SIZE = TEXT_SIZE,
                        ALIGNMENT = ALIGNMENT
                    };
                    ListAllCapitulosNonGrouped.Add(gi);
                    var exist = listCapitulos.Where(x => x.PARTE == g.PARTE).FirstOrDefault();
                    if (exist == null)
                    {
                        List<DocumentoListCellModel> list = new List<DocumentoListCellModel>();
                        foreach (var i in _listAllDocumentos.Where(x => x.TIPO == _livroSelected.TIPO && x.PARTE == g.PARTE))
                        {
                            DocumentoListCellModel item = new DocumentoListCellModel()
                            {
                                PK = i.PK,
                                PARTE = i.PARTE,
                                NUMERO = i.NUMERO,
                                REFERENCIA = i.REFERENCIA,
                                SUB_TITLE = i.SUB_TITLE,
                                TEXT = i.TEXT,
                                TIPO = i.TIPO,
                                TITLE = i.TITLE,
                                ATTRIBUTES = FontAttributes.None,
                                LINE_HEIGHT = LINE_HEIGHT,
                                REFERENCIA_SIZE = REFERENCIA_SIZE,
                                SUB_TITLE_SIZE = SUB_TITLE_SIZE,
                                TEXT_SIZE = TEXT_SIZE,
                                ALIGNMENT = ALIGNMENT
                            };
                            var exist2 = list.Where(x => x.TITLE == item.TITLE).FirstOrDefault();
                            if (exist2 == null)
                            {
                                list.Add(item);
                                listCapitulosNonGrouped.Add(item);
                            }
                        }
                        DocumentoListCellGrouped dg = new DocumentoListCellGrouped(g.PARTE, list);
                        listCapitulos.Add(dg);
                    }
                }
                ListCapitulos = new ObservableCollection<DocumentoListCellGrouped>(listCapitulos);
                if (string.IsNullOrEmpty(_parteFiltro))
                {
                    ListCapitulosNonGrouped = new ObservableCollection<DocumentoListCellModel>(listCapitulosNonGrouped.OrderBy(x => x.NUMERO));
                }
                else
                {
                    ListCapitulosNonGrouped = new ObservableCollection<DocumentoListCellModel>(listCapitulosNonGrouped.Where(x => x.PARTE.Contains(_parteFiltro)).OrderBy(x => x.NUMERO));
                }
                //if (saved != null && saved.PK > 0)
                //{
                //    //DocumentoListCellModel cs = ListCapitulos.Where(x => x.PK == saved.PK).FirstOrDefault();
                //    DocumentoListCellGrouped dlcg = ListCapitulos.Where(x => x.PARTE == saved.PARTE).FirstOrDefault();
                //    DocumentoListCellModel cs = dlcg.Where(x => x.PK == saved.PK).FirstOrDefault();
                //    CapituloSelected = cs;
                //}
                if (_linhas)
                    CapituloSelected = ListCapitulosNonGrouped.FirstOrDefault();
                else
                {
                    if (_pkUp > 0)
                    {
                        CapituloSelected = listCapitulosNonGrouped.Where(x => x.PK == _pkUp).FirstOrDefault();
                    }
                }
                IsBusy = false;
            }
        }

        public DocumentoListCellModel _capituloSelected;
        public DocumentoListCellModel CapituloSelected
        {
            get => null;
            set
            {
                if(value != null)
                {
                    foreach (var item in ListCapitulos)
                    {
                        foreach(var i in item)
                            i.ATTRIBUTES = FontAttributes.None;
                    }
                    value.ATTRIBUTES = FontAttributes.Bold;
                    SetProperty(ref _capituloSelected, value);
                    LoadLinhas();
                    PARTE = value.PARTE;
                    TIPO = value.TIPO;
                    TITLE = value.TITLE;
                    SaveCapitulo(value);
                }
            }
        }

        private void SaveCapitulo(DocumentoListCellModel value)
        {
            try
            {
                string itemJson = JsonConvert.SerializeObject(value);
                Preferences.Set(Settings.CapituloSalvo, itemJson);
            }
            catch { }
        }

        #region LINHAS
        private ObservableCollection<DocumentoListCellModel> _listLinhas = new ObservableCollection<DocumentoListCellModel>();
        public ObservableCollection<DocumentoListCellModel> ListLinhas
        {
            get { return _listLinhas ?? (_listLinhas = new ObservableCollection<DocumentoListCellModel>()); }
            set { SetProperty(ref _listLinhas, value); }
        }

        async Task LoadLinhas()
        {
            ListLinhas.Clear();
            List<DocumentoListCellModel> listLinhas = new List<DocumentoListCellModel>();
            foreach (var i in _listAllDocumentos.Where(x => x.TIPO == _livroSelected.TIPO && x.TITLE == _capituloSelected.TITLE))
            {
                DocumentoListCellModel item = new DocumentoListCellModel()
                {
                    PK = i.PK,
                    PARTE = i.PARTE,
                    NUMERO = i.NUMERO,
                    REFERENCIA = i.REFERENCIA,
                    SUB_TITLE = i.SUB_TITLE,
                    TEXT = i.TEXT,
                    TIPO = i.TIPO,
                    TITLE = i.TITLE,
                    ATTRIBUTES = FontAttributes.None,
                    LINE_HEIGHT = LINE_HEIGHT,
                    REFERENCIA_SIZE = REFERENCIA_SIZE,
                    SUB_TITLE_SIZE = SUB_TITLE_SIZE,
                    TEXT_SIZE = TEXT_SIZE,
                    ALIGNMENT = ALIGNMENT
                };
                listLinhas.Add(item);
            }
            ListLinhas = new ObservableCollection<DocumentoListCellModel>(listLinhas);
            IsBusy = false;
        }
        #endregion
        #endregion
        #region LIVROS
        private ObservableCollection<DocumentoListCellModel> _listDocumentos = new ObservableCollection<DocumentoListCellModel>();
        public ObservableCollection<DocumentoListCellModel> ListDocumentos
        {
            get { return _listDocumentos ?? (_listDocumentos = new ObservableCollection<DocumentoListCellModel>()); }
            set { SetProperty(ref _listDocumentos, value); }
        }
        //RefreshCommand
        public ICommand RefreshLivrosCommand => new Command(async () => await LoadLivros());

        async Task LoadLivros()
        {
            ListDocumentos.Clear();
            if (ModoOffline)
                _listAllDocumentos = DbHelper.GetAllDocumentos();
            else 
                _listAllDocumentos = await API.GetAllDocumentos();

            List<DocumentoListCellModel> listLivros = new List<DocumentoListCellModel>();
            foreach (var item in _listAllDocumentos.OrderBy(x => x.TIPO))
            {
                var exist = listLivros.Where(x => x.TIPO == item.TIPO && x.PARTE == item.PARTE).FirstOrDefault();
                if (exist == null)
                {
                    DocumentoListCellModel i = new DocumentoListCellModel()
                    {
                        PK = item.PK,
                        PARTE = item.PARTE,
                        NUMERO = item.NUMERO,
                        REFERENCIA = item.REFERENCIA,
                        SUB_TITLE = item.SUB_TITLE,
                        TEXT = item.TEXT,
                        TIPO = item.TIPO,
                        TITLE = item.TITLE,
                        ATTRIBUTES = FontAttributes.None
                    };
                    listLivros.Add(i);
                }
            }
            ListDocumentos = new ObservableCollection<DocumentoListCellModel>(listLivros);
            if (string.IsNullOrEmpty(_parteFiltro))
            {
                LivroSelected = ListDocumentos.Where(x => x.TIPO == _pageName).FirstOrDefault();
            }
            else
            {
                LivroSelected = ListDocumentos.Where(x => x.TIPO == _pageName && x.PARTE.Contains(_parteFiltro)).FirstOrDefault();
            }
            IsBusy = false;
        }

        private DocumentoListCellModel _livroSelected;
        public DocumentoListCellModel LivroSelected
        {
            get => null;
            set
            {
                if(value != null)
                {
                    foreach (var item in ListDocumentos)
                        item.ATTRIBUTES = FontAttributes.None;
                    value.ATTRIBUTES = FontAttributes.Bold;
                    SetProperty(ref _livroSelected, value);
                    OnPropertyChanged(nameof(LivroSelected));
                    TIPO = value.TIPO;
                    LoadCapitulos();
                }
            }
        }
        #endregion

        //PreviousCommand
        public ICommand PreviousCommand => new Command(() => Anterior());

        void Anterior()
        {
            try
            {
                //DocumentoListCellGrouped dlcg = ListCapitulos.Where(x => x.PARTE == _capituloSelected.PARTE).FirstOrDefault();
                //if (dlcg.FirstOrDefault() == _capituloSelected)
                //{
                //    if (ListCapitulos.FirstOrDefault() != dlcg)
                //    {
                //        DocumentoListCellGrouped dlcg_ant = ListCapitulos.ElementAt(ListCapitulos.IndexOf(dlcg) - 1);
                //        CapituloSelected = dlcg_ant.LastOrDefault();
                //    }
                //}
                //else
                //{
                //    CapituloSelected = dlcg.ElementAt(dlcg.IndexOf(_capituloSelected) - 1);
                //}
                CapituloSelected = ListAllCapitulosNonGrouped.Where(x => x.TIPO == _tipoSelected && x.NUMERO == (_capituloSelected.NUMERO - 1)).FirstOrDefault();
            }
            catch { }
        }
        public ICommand NextCommand => new Command(() => Proximo());

        void Proximo()
        {
            try
            {
                //DocumentoListCellGrouped dlcg = ListCapitulos.Where(x => x.PARTE == _capituloSelected.PARTE).FirstOrDefault();
                //if(dlcg.LastOrDefault() == _capituloSelected)
                //{
                //    if (ListCapitulos.LastOrDefault() != dlcg)
                //    {
                //        DocumentoListCellGrouped dlcg_post = ListCapitulos.ElementAt(ListCapitulos.IndexOf(dlcg) + 1);
                //        CapituloSelected = dlcg_post.FirstOrDefault();
                //    }
                //}
                //else
                //{
                //    CapituloSelected = dlcg.ElementAt(dlcg.IndexOf(_capituloSelected) + 1);
                //}
                CapituloSelected = ListAllCapitulosNonGrouped.Where(x => x.TIPO == _tipoSelected && x.NUMERO == (_capituloSelected.NUMERO + 1)).FirstOrDefault();
            }
            catch { }
        }
        private string _parteSelected;
        public string PARTE
        {
            get => _parteSelected;
            set => SetProperty(ref _parteSelected, value);
        }
        private string _tipoSelected = "";
        public string TIPO
        {
            get => _tipoSelected;
            set => SetProperty(ref _tipoSelected, value);
        }
        private string _titleSelected;
        public string TITLE
        {
            get => _titleSelected;
            set => SetProperty(ref _titleSelected, value);
        }

        public int PARTE_SIZE
        {
            get
            {
                return 11 + FontSizeAtual;
            }
            set
            {
                OnPropertyChanged(nameof(PARTE_SIZE));
            }
        }
        public int REFERENCIA_SIZE
        {
            get
            {
                return 12 + FontSizeAtual;
            }
            set
            {
                OnPropertyChanged(nameof(REFERENCIA_SIZE));
            }
        }
        public int TEXT_SIZE
        {
            get
            {
                return 16 + FontSizeAtual;
            }
            set
            {
                OnPropertyChanged(nameof(TEXT_SIZE));
            }
        }
        public int SUB_TITLE_SIZE
        {
            get
            {
                return 16 + FontSizeAtual;
            }
            set
            {
                OnPropertyChanged(nameof(SUB_TITLE_SIZE));
            }
        }
        public int TITLE_SIZE
        {
            get
            {
                return 16 + FontSizeAtual;
            }
            set
            {
                OnPropertyChanged(nameof(TITLE_SIZE));
            }
        }
        public double LINE_HEIGHT
        {
            get
            {
                double d = 1;
                return Preferences.Get(Settings.LineHeight, d);
            }
            set
            {
                Preferences.Set(Settings.LineHeight, value);
            }
        }

        public TextAlignment ALIGNMENT
        {
            get
            {
                string preferences = Preferences.Get(Settings.TextAlignment, "Start");
                TextAlignment ta = TextAlignment.Start;
                if (preferences == "Start")
                    ta = TextAlignment.Start;
                else if (preferences == "Center")
                    ta = TextAlignment.Center;
                else if (preferences == "End")
                    ta = TextAlignment.End;
                return ta;
            }
            set
            {
                string ta = "Start";
                if (value == TextAlignment.Start)
                    ta = "Start";
                else if (value == TextAlignment.Center)
                    ta = "Center";
                else if (value == TextAlignment.End)
                    ta = "End";
                Preferences.Set(Settings.TextAlignment, ta);
            }
        }

        public int FontSizeAtual
        {
            get => Preferences.Get(Settings.FontSize, 0);
        }

        void UpdateLinhas()
        {
            foreach (var item in ListLinhas)
            {
                item.TEXT_SIZE = TEXT_SIZE;
                item.SUB_TITLE_SIZE = SUB_TITLE_SIZE;
                item.REFERENCIA_SIZE = REFERENCIA_SIZE;
                item.LINE_HEIGHT = LINE_HEIGHT;
                item.ALIGNMENT = ALIGNMENT;
            }
        }

        public ICommand DiminuirLetraCommand
        {
            get
            {
                return new Command(() =>
                {
                    int fs = FontSizeAtual;
                    fs--;
                    Preferences.Set(Settings.FontSize, fs);
                    OnPropertyChanged(nameof(TITLE_SIZE));
                    OnPropertyChanged(nameof(SUB_TITLE_SIZE));
                    OnPropertyChanged(nameof(TEXT_SIZE));
                    OnPropertyChanged(nameof(REFERENCIA_SIZE));
                    OnPropertyChanged(nameof(PARTE_SIZE));
                    UpdateLinhas();
                });
            }
        }

        public ICommand AumentarLetraCommand
        {
            get
            {
                return new Command(() =>
                {
                    int fs = FontSizeAtual;
                    fs++;
                    Preferences.Set(Settings.FontSize, fs);
                    OnPropertyChanged(nameof(TITLE_SIZE));
                    OnPropertyChanged(nameof(SUB_TITLE_SIZE));
                    OnPropertyChanged(nameof(TEXT_SIZE));
                    OnPropertyChanged(nameof(REFERENCIA_SIZE));
                    OnPropertyChanged(nameof(PARTE_SIZE));
                    UpdateLinhas();
                });
            }
        }

        public ICommand DiminuirLineHeightCommand
        {
            get
            {
                return new Command(() =>
                {
                    double lh = LINE_HEIGHT;
                    lh = lh - 0.1;
                    Preferences.Set(Settings.LineHeight, lh);
                    OnPropertyChanged(nameof(LINE_HEIGHT));
                    UpdateLinhas();
                });
            }
        }

        public ICommand AumentarLineHeightCommand
        {
            get
            {
                return new Command(() =>
                {
                    double lh = LINE_HEIGHT;
                    lh = lh + 0.1;
                    Preferences.Set(Settings.LineHeight, lh);
                    OnPropertyChanged(nameof(LINE_HEIGHT));
                    UpdateLinhas();
                });
            }
        }

        //AlignLeftCommand
        public ICommand AlignLeftCommand
        {
            get
            {
                return new Command(() =>
                {
                    ALIGNMENT = TextAlignment.Start;
                    UpdateLinhas();
                });
            }
        }

        //AlignCenterCommand
        public ICommand AlignCenterCommand
        {
            get
            {
                return new Command(() =>
                {
                    ALIGNMENT = TextAlignment.Center;
                    UpdateLinhas();
                });
            }
        }

        //AlignRightCommand
        public ICommand AlignRightCommand
        {
            get
            {
                return new Command(() =>
                {
                    ALIGNMENT = TextAlignment.End;
                    UpdateLinhas();
                });
            }
        }
        //TextOptionsCommand
        public ICommand TextOptionsCommand
        {
            get
            {
                return new Command(() =>
                {
                    TextOptionsVisible = !TextOptionsVisible;
                });
            }
        }

        private bool _textOptionsVisible = false;
        public bool TextOptionsVisible
        {
            get => _textOptionsVisible;
            set => SetProperty(ref _textOptionsVisible, value);
        }

    }
}
