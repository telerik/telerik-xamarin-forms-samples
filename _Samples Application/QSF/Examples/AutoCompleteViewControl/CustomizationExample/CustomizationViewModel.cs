using QSF.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace QSF.Examples.AutoCompleteViewControl.CustomizationExample
{
    public class CustomizationViewModel : ExampleViewModel
    {
        public CustomizationViewModel()
        {
            this.JobCategories = new ObservableCollection<JobCategory>()
            {
                new JobCategory(Color.FromHex("#F8D4D9"), Color.FromHex("#C62F46"), "\uE818", "UX Designer"),
                new JobCategory(Color.FromHex("#F8D4D9"), Color.FromHex("#C62F46"), "\uE818", "Graphic Designer"),
                new JobCategory(Color.FromHex("#F8D4D9"), Color.FromHex("#C62F46"), "\uE818", "Web Designer"),
                new JobCategory(Color.FromHex("#D4F2DF"), Color.FromHex("#166D33"), "\uE854", "Developer"),
                new JobCategory(Color.FromHex("#D4F2DF"), Color.FromHex("#166D33"), "\uE854", "ASP .Net Developer"),
                new JobCategory(Color.FromHex("#D4F2DF"), Color.FromHex("#166D33"), "\uE854", "CRM Developer"),
                new JobCategory(Color.FromHex("#D4F2DF"), Color.FromHex("#166D33"), "\uE854", "Java Developer"),
                new JobCategory(Color.FromHex("#D4F2DF"), Color.FromHex("#166D33"), "\uE854", "Front-end Developer"),
                new JobCategory(Color.FromHex("#F0E8FF"), Color.FromHex("#5E31B1"), "\uE856", "Operations Manager"),
                new JobCategory(Color.FromHex("#F0E8FF"), Color.FromHex("#5E31B1"), "\uE856", "Project Development Manager"),
                new JobCategory(Color.FromHex("#F0E8FF"), Color.FromHex("#5E31B1"), "\uE856", "Delivery Manager"),
                new JobCategory(Color.FromHex("#E8E8EA"), Color.FromHex("#1F2833"), "\uE858", "Business Development Representative"),
                new JobCategory(Color.FromHex("#E8E8EA"), Color.FromHex("#1F2833"), "\uE858", "Business Intelligence Representative"),
                new JobCategory(Color.FromHex("#FFE3DB"), Color.FromHex("#F05C3F"), "\uE855", "Business Analyst"),
                new JobCategory(Color.FromHex("#FFE3DB"), Color.FromHex("#F05C3F"), "\uE855", "Business Intelligence Architect"),
                new JobCategory(Color.FromHex("#E3F5FF"), Color.FromHex("#1F9FD5"), "\uE859", "SAP Consultant"),
                new JobCategory(Color.FromHex("#E3F5FF"), Color.FromHex("#1F9FD5"), "\uE859", "SAP Business Analyst"),
                new JobCategory(Color.FromHex("#FFEAED"), Color.FromHex("#ED3B56"), "\uE85A", "DBA"),
                new JobCategory(Color.FromHex("#FFEAED"), Color.FromHex("#ED3B56"), "\uE85A", "Oracle Database Administrator"),
                new JobCategory(Color.FromHex("#FFF5DB"), Color.FromHex("#F5A103"), "\uE857", "Network Administrtor"),
                new JobCategory(Color.FromHex("#FFF5DB"), Color.FromHex("#F5A103"), "\uE857", "Network Engineering Specialist")
            };

            this.JobPositions = new ObservableCollection<JobPosition>()
            {
                new JobPosition("Front-End Developer","As a Front-End Developer, you will be responsible for the look, feel and navigation of complex enterprise ecommerce solutions as a member of high energy and collaborative team."),
                new JobPosition("UX Architect","We’re looking for a UX Architect for our UX/UI team who tells cohesive, well-thought and user tested stories through mind maps, user flows and ultimately, prototypes. They are able to gather requirements from concerned departments, work together with the team and create a compelling presentation to stakeholders."),
                new JobPosition("UI/UXDesigner","Translate concepts into cross-device UI mockups and storyboards. Define consistent UI style guides and page layouts. Work closely with the development team to assist them with the optimal implementation of the UI mockups, storyboards and style guides"),
                new JobPosition("Web Developer","Develop solutions for Accenture clients, typically based on the extension of the SFCC leading platform. Produce complete, correct, reliable, readable and scalable code as per requirements"),
                new JobPosition("UX Architect -Mobile","We’re looking for a UX Architect for our UX/UI team who tells cohesive, well-thought and user tested stories through mind maps, user flows and ultimately, prototypes. They will function as an embedded resource within the agile mobile engineering teams, reporting directly to the Head of UX/UI."),
                new JobPosition("UX Copywriter & Designer","You will be speaking to customers, using data to inform your designs, and, since we believe in design as a team effort, have an open work process with regular design critiques and peer feedback.")
            };
        }

        public ObservableCollection<JobCategory> JobCategories { get; set; }

        public ObservableCollection<JobPosition> JobPositions { get; set; }
    }
}