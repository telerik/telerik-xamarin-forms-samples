using System.Collections.Generic;
using QSF.ViewModels;

namespace QSF.Examples.BorderControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        public FirstLookViewModel()
        {
            this.Avatar = "Border_User.png";
            this.Person1Avatar = "Border_Person_1.png";
            this.Person2Avatar = "Border_Person_2.png";
            this.Person3Avatar = "Border_Person_3.png";
            this.Person4Avatar = "Border_Person_4.png";
        }

        public List<Comment> Comments { get; private set; }

        public string Avatar { get; set; }

        public string Person1Avatar { get; set; }
        public string Person2Avatar { get; set; }
        public string Person3Avatar { get; set; }
        public string Person4Avatar { get; set; }
    }
}

