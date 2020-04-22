using IRB.Data;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace IRB.Services
{
    public class DBHelper
    {
        LiteDatabase _dataBase;
        protected LiteCollection<Documento> Documentos;
        public DBHelper()
        {
            _dataBase = new LiteDatabase(DependencyService.Get<ILiteDBFileHelper>().GetLocalFilePath("IRB.db"));
            //_dataBase = new LiteDBHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XamarinLiteDB.db"));
            using (_dataBase)
            {
                Documentos = _dataBase.GetCollection<Documento>("Documentos");
            }
            var mapper = BsonMapper.Global;
            mapper.Entity<Documento>().Id(x => x.PK);
            //mapper.Entity & lt; Todo & gt; ()
            //          .Id(x = &gt; x.ID);
        }

        public List<Documento> GetAllDocumentos()
        {
            var documents = new List<Documento>(Documentos.FindAll());
            return documents;
        }
        public Documento GetDocumentoByPK(int pk)
        {
            var documents = GetAllDocumentos().Where(o => o.PK == pk).FirstOrDefault();
            return documents;
        }
        public void InsertBulkDocumentos(IEnumerable<Documento> bulk)
        {
            Documentos.Upsert(bulk);
        }
        public void UpsertDocumento(Documento ob)
        {
            Documentos.Upsert(ob);
        }
        public void DeleteDocumento(Documento ob)
        {
            //Documentos.Delete(o => o.PK == ob.PK);
        }
        public void DeleteAllDocumento(Documento ob)
        {
            //Documentos.DeleteAll();
        }
    }
}
