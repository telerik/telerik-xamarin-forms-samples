namespace QSF.ViewModels
{
    public class InfoViewSettings
    {
        public InfoViewSettings(InfoType type, string header, string content, string hyperlinkText = null, string logoImage = null)
        {
            this.Type = type;
            this.Header = header;
            this.Content = content;
            this.HyperlinkText = hyperlinkText;
            this.LogoImage = logoImage;
        }

        public InfoType Type { get; private set; }

        public string Header { get; private set; }

        public string Content { get; private set; }

        public string HyperlinkText { get; private set; }

        public string LogoImage { get; private set; }
    }
}