using tagit.Common;

namespace tagit.Models
{
    /// <summary>
    ///     Used to store image tag aggregations for analysis display
    /// </summary>
    public class TagAnalysisInformation : ObservableBase
    {
        private string _label;

        private int _value;

        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        public int Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}