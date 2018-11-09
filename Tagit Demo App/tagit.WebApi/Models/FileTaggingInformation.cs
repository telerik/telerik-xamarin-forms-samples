using System.Collections.Generic;

namespace tagit.WebApi.Models
{
    public class FileTaggingInformation
    {
        public string Caption { get; set; }
        public List<string> Tags { get; set; }
        public short Rating { get; set; }
    }
}