using tagit.Common;

namespace tagit.Models
{
    /// <summary>
    /// Used to store image category face information
    /// </summary>
    public class FaceInformation : ObservableBase
    {
        private int _age;

        private string _gender;

        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        public string Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }
    }
}