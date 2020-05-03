using IRB.Data;
using IRB.Models;
using IRB.Utils;
using IRB.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            CarregarPagina();
        }
        //public string _pageName;
        //public string _parteFiltro;
        //public bool _linhas;
        //public int _pkUp;
        public void SelecionarDocumento(string tipo = "", string parte = "", bool linhas = false, int pkUp = 0)
        {
            //await Task.Delay(20);
            //ListCapitulosNonGrouped.Clear();
            //_pageName = pageName;
            //_parteFiltro = parte;
            //_linhas = linhas;
            //_pkUp = pkUp;
            //if (ListDocumentos != null && ListDocumentos.Count > 0 && pkUp == 0)
            //{
            //    if (string.IsNullOrEmpty(parte))
            //    {
            //        LivroSelected = ListDocumentos.Where(x => x.TIPO == pageName).FirstOrDefault();
            //    }
            //    else
            //    {
            //        LivroSelected = ListDocumentos.Where(x => x.TIPO == pageName && x.PARTE.Contains(parte)).FirstOrDefault();
            //    }
            //}
            //else
            //{
            //    if (IsBusy)
            //        return;

            //    IsBusy = true;

            //    //await LoadLivros();
            //}
            try
            {
                if (linhas && !string.IsNullOrEmpty(tipo) && !string.IsNullOrEmpty(parte))
                {
                    CapituloSelected = _listAllDocumentos.Where(x => x.TIPO == tipo && x.PARTE == parte).FirstOrDefault();
                }
                if (pkUp > 0)
                {
                    CapituloSelected = _listAllDocumentos.Where(x => x.PK == pkUp).FirstOrDefault();
                }

            }
            catch { }
        }

        private bool _retornando = false;
        public bool Retornando
        {
            get => _retornando;
            set => SetProperty(ref _retornando, value);
        }

        private void LoadListSelectedNonGrouped()
        {
            if (_capituloSelected.TIPO == "Confissão Belga")
                ListAllCapitulosNonGrouped = ListCapitulosBelga;
            else if (_capituloSelected.TIPO == "Catecismo de Heidelberg")
                ListAllCapitulosNonGrouped = ListCapitulosHeidelberg;
            else if (_capituloSelected.TIPO == "Cânones de Dort")
                ListAllCapitulosNonGrouped = ListCapitulosDort;
            else if (_capituloSelected.TIPO == "Hinário Maceió")
                ListAllCapitulosNonGrouped = ListCapitulosHMaceio;
            else if (_capituloSelected.TIPO == "Livro de Culto")
                ListAllCapitulosNonGrouped = ListCapitulosLCulto;
            else if (_capituloSelected.TIPO == "Regimento")
                ListAllCapitulosNonGrouped = ListCapitulosRegimento;
            else if (_capituloSelected.TIPO == "Formas Litúrgicas")
                ListAllCapitulosNonGrouped = ListCapitulosFormas;

        }

        private async void CarregarPagina()
        {
            IsBusy = true;
            //string savedJson = Preferences.Get(Settings.CapituloSalvo, string.Empty);
            //if (!string.IsNullOrEmpty(savedJson))
            //    Retornando = true;
            //else
            //    Retornando = false;
            await LoadAllDocumentos();
            ListCapitulosBelga = new ObservableCollection<Documento>(_listAllDocumentos.Where(x => x.TIPO == "Confissão Belga"));
            ListCapitulosHeidelberg = new ObservableCollection<Documento>(_listAllDocumentos.Where(x => x.TIPO == "Catecismo de Heidelberg"));
            var listI = new ObservableCollection<Documento>();
            var listII = new ObservableCollection<Documento>();
            var listIII = new ObservableCollection<Documento>();
            foreach (var item in ListCapitulosHeidelberg)
            {
                if (item.PARTE == "Parte I - Nossos Pecados e Miséria")
                {
                    var exist = listI.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listI.Add(item);
                }
                else if (item.PARTE == "Parte II - Nossa Salvação")
                {
                    var exist = listII.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listII.Add(item);
                }
                else if (item.PARTE == "Parte III - Nossa Gratidão")
                {
                    var exist = listIII.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listIII.Add(item);
                }
            }
            ListCapitulosHeidelbergI = new ObservableCollection<Documento>(listI);
            ListCapitulosHeidelbergII = new ObservableCollection<Documento>(listII);
            ListCapitulosHeidelbergIII = new ObservableCollection<Documento>(listIII);

            ListCapitulosDort = new ObservableCollection<Documento>(_listAllDocumentos.Where(x => x.TIPO == "Cânones de Dort"));
            var listDortI = new ObservableCollection<Documento>();
            var listDortII = new ObservableCollection<Documento>();
            var listDortIII = new ObservableCollection<Documento>();
            var listDortV = new ObservableCollection<Documento>();
            foreach (var item in ListCapitulosDort)
            {
                if (item.PARTE == "I Capítulo: A Eleição e a Reprovação Divinas")
                {
                    var exist = listDortI.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listDortI.Add(item);
                }
                else if (item.PARTE == "II Capítulo: A Morte de Cristo e a Redenção do Homem Através Dela")
                {
                    var exist = listDortII.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listDortII.Add(item);
                }
                else if (item.PARTE == "III - IV Capítulo: A Corrupção do Homem, a sua Conversão a Deus e o Modo como isso Ocorre")
                {
                    var exist = listDortIII.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listDortIII.Add(item);
                }
                else if (item.PARTE == "V Capítulo: A Perseverança dos Santos")
                {
                    var exist = listDortV.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listDortV.Add(item);
                }
            }
            ListCapitulosDortI = new ObservableCollection<Documento>(listDortI);
            ListCapitulosDortII = new ObservableCollection<Documento>(listDortII);
            ListCapitulosDortIII = new ObservableCollection<Documento>(listDortIII);
            ListCapitulosDortV = new ObservableCollection<Documento>(listDortV);

            ListCapitulosHMaceio = new ObservableCollection<Documento>(_listAllDocumentos.Where(x => x.TIPO == "Hinário Maceió"));
            var listHMaceioS = new ObservableCollection<Documento>();
            var listHMaceioH = new ObservableCollection<Documento>();
            foreach (var item in ListCapitulosHMaceio)
            {
                if (item.PARTE == "Salmos")
                {
                    var exist = listHMaceioS.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listHMaceioS.Add(item);
                }
                else if (item.PARTE == "Hinos")
                {
                    var exist = listHMaceioH.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listHMaceioH.Add(item);
                }
            }
            ListCapitulosHMaceioS = new ObservableCollection<Documento>(listHMaceioS);
            ListCapitulosHMaceioH = new ObservableCollection<Documento>(listHMaceioH);


            ListCapitulosLCulto = new ObservableCollection<Documento>(_listAllDocumentos.Where(x => x.TIPO == "Livro de Culto"));
            var listLCultoS = new ObservableCollection<Documento>();
            var listLCultoH = new ObservableCollection<Documento>();
            foreach (var item in ListCapitulosLCulto)
            {
                if (item.PARTE.Contains("Saltério"))
                {
                    var exist = listLCultoS.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listLCultoS.Add(item);
                }
                else if (item.PARTE.Contains("Hinário"))
                {
                    var exist = listLCultoH.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listLCultoH.Add(item);
                }
            }
            ListCapitulosLCultoS = new ObservableCollection<Documento>(listLCultoS);
            ListCapitulosLCultoH = new ObservableCollection<Documento>(listLCultoH);

            ListCapitulosRegimento = new ObservableCollection<Documento>(_listAllDocumentos.Where(x => x.TIPO == "Regimento"));
            var listRegimentoI = new ObservableCollection<Documento>();
            var listRegimentoII = new ObservableCollection<Documento>();
            var listRegimentoIII = new ObservableCollection<Documento>();
            var listRegimentoIV = new ObservableCollection<Documento>();
            var listRegimentoV = new ObservableCollection<Documento>();
            foreach (var item in ListCapitulosRegimento)
            {
                if (item.PARTE.Contains("I. Ofícios"))
                {
                    var exist = listRegimentoI.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listRegimentoI.Add(item);
                }
                else if (item.PARTE.Contains("II. As Assembléias Eclesiásticas"))
                {
                    var exist = listRegimentoII.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listRegimentoII.Add(item);
                }
                else if (item.PARTE.Contains("III. Os Cultos Públicos E Os Sacramentos"))
                {
                    var exist = listRegimentoIII.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listRegimentoIII.Add(item);
                }
                else if (item.PARTE.Contains("IV. A Disciplina Eclesiástica"))
                {
                    var exist = listRegimentoIV.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listRegimentoIV.Add(item);
                }
                else if (item.PARTE.Contains("V. Os Artigos Finais"))
                {
                    var exist = listRegimentoV.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                    if (exist == null)
                        listRegimentoV.Add(item);
                }
            }
            ListCapitulosRegimentoI = new ObservableCollection<Documento>(listRegimentoI);
            ListCapitulosRegimentoII = new ObservableCollection<Documento>(listRegimentoII);
            ListCapitulosRegimentoIII = new ObservableCollection<Documento>(listRegimentoIII);
            ListCapitulosRegimentoIV = new ObservableCollection<Documento>(listRegimentoIV);
            ListCapitulosRegimentoV = new ObservableCollection<Documento>(listRegimentoV);

            var listFormas = new ObservableCollection<Documento>();
            foreach (var item in _listAllDocumentos.Where(x => x.TIPO == "Formas Litúrgicas"))
            {
                var exist = listFormas.Where(x => x.NUMERO == item.NUMERO).FirstOrDefault();
                if (exist == null)
                    listFormas.Add(item);
            }
            ListCapitulosFormas = new ObservableCollection<Documento>(listFormas);

            IsBusy = false;
            //await LoadLivros();
            //if (!string.IsNullOrEmpty(savedJson))
            //{
            //    saved = JsonConvert.DeserializeObject<DocumentoListCellModel>(savedJson);
            //    LivroSelected = saved;
            //    //var cs = _listDocumentos.Where(i => i.PK == saved.PK).FirstOrDefault();
            //    //CapituloSelected = cs;
            //}
        }
        //DocumentoListCellModel saved;

        public IEnumerable<Documento> _listAllDocumentos;

        #region CAPITULOS
        //private ObservableCollection<DocumentoListCellGrouped> _listCapitulos = new ObservableCollection<DocumentoListCellGrouped>();
        //public ObservableCollection<DocumentoListCellGrouped> ListCapitulos
        //{
        //    get { return _listCapitulos ?? (_listCapitulos = new ObservableCollection<DocumentoListCellGrouped>()); }
        //    set { SetProperty(ref _listCapitulos, value); }
        //}
        private ObservableCollection<Documento> _listCapitulosBelga = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosBelga
        {
            get { return _listCapitulosBelga ?? (_listCapitulosBelga = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosBelga, value); }
        }
        private ObservableCollection<Documento> _listCapitulosHeidelberg = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosHeidelberg
        {
            get { return _listCapitulosHeidelberg ?? (_listCapitulosHeidelberg = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosHeidelberg, value); }
        }
        private ObservableCollection<Documento> _listCapitulosHeidelbergII = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosHeidelbergII
        {
            get { return _listCapitulosHeidelbergII ?? (_listCapitulosHeidelbergII = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosHeidelbergII, value); }
        }
        private ObservableCollection<Documento> _listCapitulosHeidelbergIII = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosHeidelbergIII
        {
            get { return _listCapitulosHeidelbergIII ?? (_listCapitulosHeidelbergIII = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosHeidelbergIII, value); }
        }
        private ObservableCollection<Documento> _listCapitulosHeidelbergI = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosHeidelbergI
        {
            get { return _listCapitulosHeidelbergI ?? (_listCapitulosHeidelbergI = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosHeidelbergI, value); }
        }
        private ObservableCollection<Documento> _listCapitulosDort = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosDort
        {
            get { return _listCapitulosDort ?? (_listCapitulosDort = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosDort, value); }
        }
        private ObservableCollection<Documento> _listCapitulosDortI = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosDortI
        {
            get { return _listCapitulosDortI ?? (_listCapitulosDortI = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosDortI, value); }
        }
        private ObservableCollection<Documento> _listCapitulosDortII = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosDortII
        {
            get { return _listCapitulosDortII ?? (_listCapitulosDortII = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosDortII, value); }
        }
        private ObservableCollection<Documento> _listCapitulosDortIII = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosDortIII
        {
            get { return _listCapitulosDortIII ?? (_listCapitulosDortIII = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosDortIII, value); }
        }
        private ObservableCollection<Documento> _listCapitulosDortV = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosDortV
        {
            get { return _listCapitulosDortV ?? (_listCapitulosDortV = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosDortV, value); }
        }
        private ObservableCollection<Documento> _listCapitulosHMaceio = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosHMaceio
        {
            get { return _listCapitulosHMaceio ?? (_listCapitulosHMaceio = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosHMaceio, value); }
        }
        private ObservableCollection<Documento> _listCapitulosHMaceioS = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosHMaceioS
        {
            get { return _listCapitulosHMaceioS ?? (_listCapitulosHMaceioS = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosHMaceioS, value); }
        }
        private ObservableCollection<Documento> _listCapitulosHMaceioH = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosHMaceioH
        {
            get { return _listCapitulosHMaceioH ?? (_listCapitulosHMaceioH = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosHMaceioH, value); }
        }
        private ObservableCollection<Documento> _listCapitulosLCulto = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosLCulto
        {
            get { return _listCapitulosLCulto ?? (_listCapitulosLCulto = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosLCulto, value); }
        }
        private ObservableCollection<Documento> _listCapitulosLCultoS = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosLCultoS
        {
            get { return _listCapitulosLCultoS ?? (_listCapitulosLCultoS = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosLCultoS, value); }
        }
        private ObservableCollection<Documento> _listCapitulosLCultoH = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosLCultoH
        {
            get { return _listCapitulosLCultoH ?? (_listCapitulosLCultoH = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosLCultoH, value); }
        }
        private ObservableCollection<Documento> _listCapitulosRegimento = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosRegimento
        {
            get { return _listCapitulosRegimento ?? (_listCapitulosRegimento = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosRegimento, value); }
        }
        private ObservableCollection<Documento> _listCapitulosRegimentoI = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosRegimentoI
        {
            get { return _listCapitulosRegimentoI ?? (_listCapitulosRegimentoI = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosRegimentoI, value); }
        }
        private ObservableCollection<Documento> _listCapitulosRegimentoII = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosRegimentoII
        {
            get { return _listCapitulosRegimentoII ?? (_listCapitulosRegimentoII = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosRegimentoII, value); }
        }
        private ObservableCollection<Documento> _listCapitulosRegimentoIII = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosRegimentoIII
        {
            get { return _listCapitulosRegimentoIII ?? (_listCapitulosRegimentoIII = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosRegimentoIII, value); }
        }
        private ObservableCollection<Documento> _listCapitulosRegimentoIV = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosRegimentoIV
        {
            get { return _listCapitulosRegimentoIV ?? (_listCapitulosRegimentoIV = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosRegimentoIV, value); }
        }
        private ObservableCollection<Documento> _listCapitulosRegimentoV = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosRegimentoV
        {
            get { return _listCapitulosRegimentoV ?? (_listCapitulosRegimentoV = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosRegimentoV, value); }
        }
        private ObservableCollection<Documento> _listCapitulosFormas = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListCapitulosFormas
        {
            get { return _listCapitulosFormas ?? (_listCapitulosFormas = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listCapitulosFormas, value); }
        }
        //private ObservableCollection<DocumentoListCellModel> _listCapitulosNonGrouped = new ObservableCollection<DocumentoListCellModel>();
        //public ObservableCollection<DocumentoListCellModel> ListCapitulosNonGrouped
        //{
        //    get { return _listCapitulosNonGrouped ?? (_listCapitulosNonGrouped = new ObservableCollection<DocumentoListCellModel>()); }
        //    set { SetProperty(ref _listCapitulosNonGrouped, value); }
        //}
        private ObservableCollection<Documento> _listAllCapitulosNonGrouped = new ObservableCollection<Documento>();
        public ObservableCollection<Documento> ListAllCapitulosNonGrouped
        {
            get { return _listAllCapitulosNonGrouped ?? (_listAllCapitulosNonGrouped = new ObservableCollection<Documento>()); }
            set { SetProperty(ref _listAllCapitulosNonGrouped, value); }
        }
        //RefreshCommand
        //public ICommand RefreshCapitulosCommand => new Command(async () => await LoadCapitulos());

        //async Task LoadCapitulos()
        //{
        //    if(_listAllDocumentos != null)
        //    {
        //        ListCapitulos.Clear();
        //        List<DocumentoListCellGrouped> listCapitulos = new List<DocumentoListCellGrouped>();
        //        List<DocumentoListCellModel> listCapitulosNonGrouped = new List<DocumentoListCellModel>();
        //        //IEnumerable<Documento> listFiltered;
        //        //if (string.IsNullOrEmpty(_parteFiltro))
        //        //    listFiltered = _listAllDocumentos.Where(x => x.TIPO == _livroSelected.TIPO);
        //        //else
        //        //    listFiltered = _listAllDocumentos.Where(x => x.TIPO == _livroSelected.TIPO && x.PARTE.Contains(_parteFiltro));

        //        //foreach (var i in listFiltered)
        //        //{
        //        //    DocumentoListCellModel item = new DocumentoListCellModel()
        //        //    {
        //        //        PK = i.PK,
        //        //        PARTE = i.PARTE,
        //        //        NUMERO = i.NUMERO,
        //        //        REFERENCIA = i.REFERENCIA,
        //        //        SUB_TITLE = i.SUB_TITLE,
        //        //        TEXT = i.TEXT,
        //        //        TIPO = i.TIPO,
        //        //        TITLE = i.TITLE,
        //        //        ATTRIBUTES = FontAttributes.None,
        //        //        LINE_HEIGHT = LINE_HEIGHT,
        //        //        REFERENCIA_SIZE = REFERENCIA_SIZE,
        //        //        SUB_TITLE_SIZE = SUB_TITLE_SIZE,
        //        //        TEXT_SIZE = TEXT_SIZE,
        //        //        ALIGNMENT = ALIGNMENT
        //        //    };
        //        //    var exist2 = list.Where(x => x.TITLE == item.TITLE).FirstOrDefault();
        //        //    if (exist2 == null)
        //        //    {
        //        //        list.Add(item);
        //        //        listCapitulosNonGrouped.Add(item);
        //        //    }
        //        //}
        //        foreach (var g in _listAllDocumentos.Where(x => x.TIPO == _livroSelected.TIPO))
        //        {
        //            DocumentoListCellModel gi = new DocumentoListCellModel()
        //            {
        //                PK = g.PK,
        //                PARTE = g.PARTE,
        //                NUMERO = g.NUMERO,
        //                REFERENCIA = g.REFERENCIA,
        //                SUB_TITLE = g.SUB_TITLE,
        //                TEXT = g.TEXT,
        //                TIPO = g.TIPO,
        //                TITLE = g.TITLE,
        //                ATTRIBUTES = FontAttributes.None,
        //                LINE_HEIGHT = LINE_HEIGHT,
        //                REFERENCIA_SIZE = REFERENCIA_SIZE,
        //                SUB_TITLE_SIZE = SUB_TITLE_SIZE,
        //                TEXT_SIZE = TEXT_SIZE,
        //                ALIGNMENT = ALIGNMENT,
        //                TITLE_NUMERO = g.TITLE_NUMERO
        //            };
        //            ListAllCapitulosNonGrouped.Add(gi);
        //            var exist = listCapitulos.Where(x => x.PARTE == g.PARTE).FirstOrDefault();
        //            if (exist == null)
        //            {
        //                List<DocumentoListCellModel> list = new List<DocumentoListCellModel>();
        //                foreach (var i in _listAllDocumentos.Where(x => x.TIPO == _livroSelected.TIPO && x.PARTE == g.PARTE))
        //                {
        //                    DocumentoListCellModel item = new DocumentoListCellModel()
        //                    {
        //                        PK = i.PK,
        //                        PARTE = i.PARTE,
        //                        NUMERO = i.NUMERO,
        //                        REFERENCIA = i.REFERENCIA,
        //                        SUB_TITLE = i.SUB_TITLE,
        //                        TEXT = i.TEXT,
        //                        TIPO = i.TIPO,
        //                        TITLE = i.TITLE,
        //                        ATTRIBUTES = FontAttributes.None,
        //                        LINE_HEIGHT = LINE_HEIGHT,
        //                        REFERENCIA_SIZE = REFERENCIA_SIZE,
        //                        SUB_TITLE_SIZE = SUB_TITLE_SIZE,
        //                        TEXT_SIZE = TEXT_SIZE,
        //                        ALIGNMENT = ALIGNMENT,
        //                        TITLE_NUMERO = i.TITLE_NUMERO
        //                    };
        //                    var exist2 = list.Where(x => x.TITLE == item.TITLE).FirstOrDefault();
        //                    if (exist2 == null)
        //                    {
        //                        list.Add(item);
        //                        listCapitulosNonGrouped.Add(item);
        //                    }
        //                }
        //                DocumentoListCellGrouped dg = new DocumentoListCellGrouped(g.PARTE, list);
        //                listCapitulos.Add(dg);
        //            }
        //        }
        //        ListCapitulos = new ObservableCollection<DocumentoListCellGrouped>(listCapitulos);
        //        if (string.IsNullOrEmpty(_parteFiltro))
        //        {
        //            ListCapitulosNonGrouped = new ObservableCollection<DocumentoListCellModel>(listCapitulosNonGrouped.OrderBy(x => x.NUMERO));
        //        }
        //        else
        //        {
        //            ListCapitulosNonGrouped = new ObservableCollection<DocumentoListCellModel>(listCapitulosNonGrouped.Where(x => x.PARTE.Contains(_parteFiltro)).OrderBy(x => x.NUMERO));
        //        }
        //        //if (saved != null && saved.PK > 0)
        //        //{
        //        //    //DocumentoListCellModel cs = ListCapitulos.Where(x => x.PK == saved.PK).FirstOrDefault();
        //        //    DocumentoListCellGrouped dlcg = ListCapitulos.Where(x => x.PARTE == saved.PARTE).FirstOrDefault();
        //        //    DocumentoListCellModel cs = dlcg.Where(x => x.PK == saved.PK).FirstOrDefault();
        //        //    CapituloSelected = cs;
        //        //}
        //        if (_linhas)
        //            CapituloSelected = ListCapitulosNonGrouped.FirstOrDefault();
        //        else
        //        {
        //            if (_pkUp > 0)
        //            {
        //                CapituloSelected = listCapitulosNonGrouped.Where(x => x.PK == _pkUp).FirstOrDefault();
        //            }
        //        }
        //        IsBusy = false;
        //    }
        //}

        public Documento _capituloSelected;
        public Documento CapituloSelected
        {
            get => null;
            set
            {
                if(value != null)
                {
                    //foreach (var item in ListCapitulos)
                    //{
                    //    foreach(var i in item)
                    //        i.ATTRIBUTES = FontAttributes.None;
                    //}
                    //value.ATTRIBUTES = FontAttributes.Bold;
                    SetProperty(ref _capituloSelected, value);
                    LoadLinhasSelected();
                    PARTE = value.PARTE;
                    TITLE_NUMERO = value.TITLE_NUMERO;
                    TIPO = value.TIPO;
                    TITLE = value.TITLE;
                    LoadListSelectedNonGrouped();
                    //SaveCapitulo(value);
                }
            }
        }
        private async void LoadLinhasSelected()
        {
            await LoadLinhas();
        }

        //private void SaveCapitulo(DocumentoListCellModel value)
        //{
        //    try
        //    {
        //        string itemJson = JsonConvert.SerializeObject(value);
        //        Preferences.Set(Settings.CapituloSalvo, itemJson);
        //    }
        //    catch { }
        //}

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
            foreach (var i in _listAllDocumentos.Where(x => x.TIPO == _capituloSelected.TIPO && x.TITLE == _capituloSelected.TITLE))
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
                    ALIGNMENT = ALIGNMENT,
                    TITLE_NUMERO = i.TITLE_NUMERO
                };
                listLinhas.Add(item);
            }
            ListLinhas = new ObservableCollection<DocumentoListCellModel>(listLinhas);
            IsBusy = false;
        }
        #endregion
        #endregion
        #region LIVROS
        //private ObservableCollection<DocumentoListCellModel> _listDocumentos = new ObservableCollection<DocumentoListCellModel>();
        //public ObservableCollection<DocumentoListCellModel> ListDocumentos
        //{
        //    get { return _listDocumentos ?? (_listDocumentos = new ObservableCollection<DocumentoListCellModel>()); }
        //    set { SetProperty(ref _listDocumentos, value); }
        //}
        //RefreshCommand
        //public ICommand RefreshLivrosCommand => new Command(async () => await LoadLivros());

        public async Task LoadAllDocumentos()
        {
            if (ModoOffline)
                _listAllDocumentos = DbHelper.GetAllDocumentos();
            else
                _listAllDocumentos = await API.GetAllDocumentos();
        }

        //async Task LoadLivros()
        //{
        //    ListDocumentos.Clear();
        //    await LoadAllDocumentos();

        //    List<DocumentoListCellModel> listLivros = new List<DocumentoListCellModel>();
        //    foreach (var item in _listAllDocumentos.OrderBy(x => x.TIPO))
        //    {
        //        var exist = listLivros.Where(x => x.TIPO == item.TIPO && x.PARTE == item.PARTE).FirstOrDefault();
        //        if (exist == null)
        //        {
        //            DocumentoListCellModel i = new DocumentoListCellModel()
        //            {
        //                PK = item.PK,
        //                PARTE = item.PARTE,
        //                NUMERO = item.NUMERO,
        //                REFERENCIA = item.REFERENCIA,
        //                SUB_TITLE = item.SUB_TITLE,
        //                TITLE_NUMERO = item.TITLE_NUMERO,
        //                TEXT = item.TEXT,
        //                TIPO = item.TIPO,
        //                TITLE = item.TITLE,
        //                ATTRIBUTES = FontAttributes.None
        //            };
        //            listLivros.Add(i);
        //        }
        //    }
        //    ListDocumentos = new ObservableCollection<DocumentoListCellModel>(listLivros);
        //    if (string.IsNullOrEmpty(_parteFiltro))
        //    {
        //        LivroSelected = ListDocumentos.Where(x => x.TIPO == _pageName).FirstOrDefault();
        //    }
        //    else
        //    {
        //        LivroSelected = ListDocumentos.Where(x => x.TIPO == _pageName && x.PARTE.Contains(_parteFiltro)).FirstOrDefault();
        //    }
        //    IsBusy = false;
        //}

        //private DocumentoListCellModel _livroSelected;
        //public DocumentoListCellModel LivroSelected
        //{
        //    get => null;
        //    set
        //    {
        //        if(value != null)
        //        {
        //            foreach (var item in ListDocumentos)
        //                item.ATTRIBUTES = FontAttributes.None;
        //            value.ATTRIBUTES = FontAttributes.Bold;
        //            SetProperty(ref _livroSelected, value);
        //            OnPropertyChanged(nameof(LivroSelected));
        //            TIPO = value.TIPO;
        //            LoadCapituloSelected();
        //        }
        //    }
        //}
        //public async void LoadCapituloSelected()
        //{
        //    await LoadCapitulos();
        //}
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
        private string _titleNumeroSelected;
        public string TITLE_NUMERO
        {
            get => _titleNumeroSelected;
            set => SetProperty(ref _titleNumeroSelected, value);
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
