using System;
using System.Collections.Generic;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace ErpApp.Controls
{
    public partial class SelectablePath : TemplatedView
    {
        public static readonly BindableProperty BrushProperty = BindableProperty.Create(nameof(Brush),
            typeof(RadBrush), typeof(SelectablePath), null, defaultBindingMode: BindingMode.OneWay);
           
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected),
            typeof(bool), typeof(SelectablePath), false);

        public SelectablePath()
        {
            InitializeComponent();
        }

        public RadBrush Brush
        {
            get { return (RadBrush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
    }
}
