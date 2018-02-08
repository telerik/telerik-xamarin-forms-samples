using tagit.Common;

namespace tagit.Models
{
    /// <summary>
    ///  Used to store image category aggregations for analysis display
    /// </summary>
    public class CategoryAnalysisInformation : ObservableBase
    {
        public CategoryAnalysisInformation()
        {
            Tag1Label = string.Empty;
            Tag1Value = 0.01;
            Tag2Label = string.Empty;
            Tag2Value = 0.01;
            Tag3Label = string.Empty;
            Tag3Value = 0.01;
        }

        private string _tag1Label;

        private double _tag1Value;

        private string _tag2Label;

        private double _tag2Value;

        private string _tag3Label;

        private double _tag3Value;
        
        public string Tag1Label
        {
            get => _tag1Label;
            set => SetProperty(ref _tag1Label, value);
        }

        public double Tag1Value
        {
            get => _tag1Value;
            set => SetProperty(ref _tag1Value, value);
        }

        public string Tag2Label
        {
            get => _tag2Label;
            set => SetProperty(ref _tag2Label, value);
        }

        public double Tag2Value
        {
            get => _tag2Value;
            set => SetProperty(ref _tag2Value, value);
        }

        public string Tag3Label
        {
            get => _tag3Label;
            set => SetProperty(ref _tag3Label, value);
        }

        public double Tag3Value
        {
            get => _tag3Value;
            set => SetProperty(ref _tag3Value, value);
        }
    }
}