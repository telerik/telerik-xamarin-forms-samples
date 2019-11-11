using FreshMvvm;
using System.Runtime.CompilerServices;

namespace TodoApp
{
    public class FreshBasePageModelEx : FreshBasePageModel
    {
        public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (object.Equals(storage, value))
                return false;

            storage = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }
    }
}
