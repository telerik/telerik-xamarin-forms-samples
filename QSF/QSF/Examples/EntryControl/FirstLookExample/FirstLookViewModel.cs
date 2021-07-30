using QSF.Services.Toast;
using QSF.ViewModels;
using Xamarin.Forms;

namespace QSF.Examples.EntryControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private string firstName;
        private string username;
        private string emailAddress;
        private string phone;
        private string password;

        public FirstLookViewModel()
        {
            this.RegisterCommand = new Command(this.Register);
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                if (this.username != value)
                {
                    this.username = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string EmailAddress
        {
            get
            {
                return this.emailAddress;
            }
            set
            {
                if (this.emailAddress != value)
                {
                    this.emailAddress = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Phone
        {
            get
            {
                return this.phone;
            }
            set
            {
                if (this.phone != value)
                {
                    this.phone = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Command RegisterCommand { get; }

        private void Register()
        {            
            string registerMessage;

            if(string.IsNullOrEmpty(this.FirstName) ||
               string.IsNullOrEmpty(this.Username) ||
               string.IsNullOrEmpty(this.EmailAddress) ||
               string.IsNullOrEmpty(this.Phone) ||
               string.IsNullOrEmpty(this.Password))
            {
                registerMessage = "All fields are mandatory!";
            }
            else
            {
                registerMessage = "Your Account has been created.";
                this.FirstName = string.Empty;
                this.Username = string.Empty;
                this.EmailAddress = string.Empty;
                this.Phone = string.Empty;
                this.Password = string.Empty;
            }

            var toastService = DependencyService.Get<IToastMessageService>();
            toastService.ShortAlert(registerMessage);
        }
    }
}

