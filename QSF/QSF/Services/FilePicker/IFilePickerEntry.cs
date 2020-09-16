using System;
using System.IO;

namespace QSF.Services
{
    public interface IFilePickerEntry : IDisposable
    {
        string FileName { get; }
        string FilePath { get; }

        Stream OpenFile();
    }
}