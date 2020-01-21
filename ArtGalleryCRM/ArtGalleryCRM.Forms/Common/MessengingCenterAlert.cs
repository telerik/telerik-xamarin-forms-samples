using System;

namespace ArtGalleryCRM.Forms.Common
{
    public class MessagingCenterAlert
    {
        public string Title { get; set; }
        
        public string Message { get; set; }
        
        public string Cancel { get; set; }
        
        public Action OnCompleted { get; set; }
    }
}