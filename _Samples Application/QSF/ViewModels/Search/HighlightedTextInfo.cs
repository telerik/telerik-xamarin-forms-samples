namespace QSF.ViewModels
{
    public class HighlightedTextInfo
    {
        public HighlightedTextInfo(string text, int firstCharIndex, int lastCharIndex)
        {
            this.Text = text;
            this.FirstCharIndex = firstCharIndex;
            this.Lenght = lastCharIndex - firstCharIndex;
        }

        public string Text { get; }

        public int FirstCharIndex { get; }

        public int Lenght { get; }
    }
}
