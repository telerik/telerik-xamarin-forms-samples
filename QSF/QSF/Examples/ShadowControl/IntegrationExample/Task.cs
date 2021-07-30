namespace QSF.Examples.ShadowControl.IntegrationExample
{
    public class Task
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Date { get; set; }
        public string Color { get; set; }
        public string Abbreviation
        {
            get
            {
                if (string.IsNullOrEmpty(this.Title))
                {
                    return string.Empty;
                }

                return string.Format("{0}", this.Title[0]);
            }
        }
    }
}
