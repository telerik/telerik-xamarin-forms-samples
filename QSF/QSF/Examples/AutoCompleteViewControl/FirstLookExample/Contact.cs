using Xamarin.Forms;

namespace QSF.Examples.AutoCompleteViewControl.FirstLookExample
{
    public class Contact
    {
        public Contact(string name, string email, Color color)
        {
            this.Name = name;
            this.Email = email;
            this.Color = color;
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public Color Color { get; set; }
    }
}
