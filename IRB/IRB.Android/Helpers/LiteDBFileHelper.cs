using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IRB.Droid.Helpers;
using IRB.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(LiteDBFileHelper))]
namespace IRB.Droid.Helpers
{
    public class LiteDBFileHelper : ILiteDBFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            
            string fullPath = Path.Combine(path, filename);
            if (!File.Exists(fullPath))
            {
                File.Create(fullPath).Dispose();
            }

            return fullPath;
        }
    }
}