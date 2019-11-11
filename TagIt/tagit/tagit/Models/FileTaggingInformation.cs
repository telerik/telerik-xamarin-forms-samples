using System.Collections.Generic;

namespace tagit
{
    /// <summary>
    ///  Used to store initial tagging information
    /// </summary>
    public class FileTaggingInformation
    {
        public string Caption { get; set; }
        public List<string> Tags { get; set; }
        public short Rating { get; set; }
    }
}