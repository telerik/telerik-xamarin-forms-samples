using QSF.Services;
using QSF.Services.Toast;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Telerik.XamarinForms.RichTextEditor;
using Xamarin.Forms;

namespace QSF.Examples.RichTextEditorControl.CustomImagePickerExample
{
    public class CustomImagePickerViewModel : ExampleViewModel
    {
        public CustomImagePickerViewModel()
        {
            var resourceService = DependencyService.Get<IResourceService>();
            this.RecipientsItemSource = new List<string>
            {
                "BrandonPeterson",
                "VacationWander",
                "Joshua Price",
                "Reuben Holmes",
                "Eva Lawson",
                "Rory Baxter",
                "David Webb",
                "Howard Pittman",
            };
            this.Source = RichTextSource.FromStream(() => resourceService.GetResourceStream("pick-image-demo.html"));
            //this.EmojiSource =
            this.EmojiSource = resourceService
                               .GetResourceNamesFromFolder("Emojis")
                               .OrderBy(resourceName => resourceName)
                               .Select(resourceName => RichTextImageSource.FromStream(() =>
                                        resourceService.GetResourceStream(resourceName), RichTextImageType.Png))
                               .ToList();


            this.SendEmailCommand = new Command(SendEmail);
        }
        public RichTextSource Source { get; }

        public List<string> RecipientsItemSource { get; }
        public List<RichTextImageSource> EmojiSource { get; }

        public ICommand SendEmailCommand { get; }

        private void SendEmail()
        {
            var toastMessage = "Email sent.";
            var toastService = DependencyService.Get<IToastMessageService>();

            toastService.ShortAlert(toastMessage);
        }
    }
}
