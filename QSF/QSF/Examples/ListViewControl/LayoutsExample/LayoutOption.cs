namespace QSF.Examples.ListViewControl.LayoutsExample
{
    public class LayoutOption
    {
        public LayoutType Type { get; private set; }
        public string Image { get; private set; }

        public LayoutOption(LayoutType type, string image)
        {
            this.Type = type;
            this.Image = image;
        }
    }
}
