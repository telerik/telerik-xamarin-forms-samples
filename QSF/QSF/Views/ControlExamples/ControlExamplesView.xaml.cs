namespace QSF.Views
{
    public partial class ControlExamplesView : ExamplesViewBase
    {
        public ControlExamplesView()
        {
            this.InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            this.examplesList.UpdateListViewLayoutDefinition(160, 6, 10, 12);
        }
    }
}