namespace QSF.ViewModels
{
    public class ExampleInfo
    {
        public ExampleInfo(string controlName, string exampleName)
        {
            this.ControlName = controlName;
            this.ExampleName = exampleName;
        }

        public string ControlName { get; }

        public string ExampleName { get; }
    }
}
