using QSF.Services.DeviceInfo;
using System;
using System.Text;
using Telerik.XamarinForms.DataControls;
using Xamarin.Forms;

namespace QSF
{
    public static class Extensions
    {
        public static string InsertSpacesInPascalCase(this string pascalCaseInput)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < pascalCaseInput.Length; i++)
            {
                if (i > 0 && char.IsUpper(pascalCaseInput[i]))
                {
                    sb.Append(' ');
                }

                sb.Append(pascalCaseInput[i]);
            }

            return sb.ToString();
        }

        public static void UpdateListViewLayoutDefinition(this RadListView listView, int imageWidth, int listViewMargins, int padding, int itemSpacing)
        {
            var definition = listView.LayoutDefinition as Telerik.XamarinForms.DataControls.ListView.ListViewGridLayout;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    itemSpacing /= 2;
                    break;
                case Device.UWP:
                    itemSpacing = itemSpacing * 2 + 25; // because of native control margins and paddings
                    break;
            }

            IDeviceInfoService deviceInfo = DependencyService.Get<IDeviceInfoService>();

            var screenSize = deviceInfo.GetScreenSize();

            definition.SpanCount = (int)Math.Floor(((screenSize.Width / deviceInfo.PixelDensity) - listViewMargins) / (imageWidth + 2 * padding + itemSpacing) + 0.03);
        }
    }
}
