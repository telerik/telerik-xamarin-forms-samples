using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp
{
    public interface IDbFileProvider
    {
        string GetLocalFilePath(string filename);
    }
}
