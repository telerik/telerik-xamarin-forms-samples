using System.IO;
using Plugin.FilePicker.Abstractions;

namespace QSF.Services
{
    public class FilePickerEntry : IFilePickerEntry
    {
        private readonly FileData fileData;

        public FilePickerEntry(FileData fileData)
        {
            this.fileData = fileData;
        }

        public string FileName
        {
            get
            {
                return this.fileData.FileName;
            }
        }

        public string FilePath
        {
            get
            {
                return this.fileData.FilePath;
            }
        }

        public Stream OpenFile()
        {
            return this.fileData.GetStream();
        }

        public void Dispose()
        {
            this.fileData.Dispose();
        }
    }
}
