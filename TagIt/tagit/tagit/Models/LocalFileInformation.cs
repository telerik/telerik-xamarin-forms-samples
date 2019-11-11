using System;

namespace tagit
{
    public class LocalFileInformation
    {
        public string FileName { get; set; }
        public string Url { get; set; }
        public byte[] File { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}