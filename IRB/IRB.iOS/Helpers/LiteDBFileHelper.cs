using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using IRB.iOS.Helpers;
using IRB.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(LiteDBFileHelper))]
namespace IRB.iOS.Helpers
{
    class LiteDBFileHelper : ILiteDBFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
                Directory.CreateDirectory(libFolder);

            return Path.Combine(libFolder, filename);
        }
    }
}