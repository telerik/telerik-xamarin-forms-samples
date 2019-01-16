namespace QSF.Examples.AutoCompleteViewControl.CustomizationExample
{
    public class JobPosition
    {
        public JobPosition(string title, string description)
        {
            this.Title = title;
            this.Description = description;
        }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}