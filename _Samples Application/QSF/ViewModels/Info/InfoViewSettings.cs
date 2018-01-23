namespace QSF.ViewModels
{
    public class InfoViewSettings
    {
        public InfoViewSettings(InfoType type, string header, string content)
        {
            this.Type = type;
            this.Header = header;
            this.Content = content;
        }

        public InfoType Type { get; private set; }

        public string Header { get; private set; }

        public string Content { get; private set; }
    }
}
