using System;
using System.Collections.Generic;
using System.Text;

namespace IRB.Services
{
    public interface ILiteDBFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
