using System;
using System.Collections.Generic;
using System.Linq;

using Android.Content;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(ErpApp.Droid.Renderers.BottomNavTabPageRenderer))]

namespace ErpApp.Droid.Renderers
{
    public class BottomNavTabPageRenderer : TabbedPageRenderer
    {
        public BottomNavTabPageRenderer(Context context)
            : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            var childViews = GetAllChildViews(ViewGroup);

            foreach (var childView in childViews)
            {
                if (childView is BottomNavigationView bottomNavigationView)
                {
                    DisableShiftMode(bottomNavigationView);
                }
            }
        }

        List<View> GetAllChildViews(View view)
        {
            if (!(view is ViewGroup group))
            {
                return new List<View> { view };
            }

            var result = new List<View>();

            for (int i = 0; i < group.ChildCount; i++)
            {
                var child = group.GetChildAt(i);

                var childList = new List<View> { child };
                childList.AddRange(GetAllChildViews(child));

                result.AddRange(childList);
            }

            return result.Distinct().ToList();
        }

        public void DisableShiftMode(BottomNavigationView bottomNavigationView)
        {
            try
            {
                var menuView = bottomNavigationView.GetChildAt(0) as BottomNavigationMenuView;
                if (menuView == null)
                {
                    System.Diagnostics.Debug.WriteLine("Unable to find BottomNavigationMenuView");
                    return;
                }

                var shiftMode = menuView.Class.GetDeclaredField("mShiftingMode");
                shiftMode.Accessible = true;
                shiftMode.SetBoolean(menuView, false);
                shiftMode.Accessible = false;
                shiftMode.Dispose();

                menuView.ChildViewAdded += OnChildViewAdded;

                for (int i = 0; i < menuView.ChildCount; i++)
                {
                    if (!(menuView.GetChildAt(i) is BottomNavigationItemView item))
                        continue;

                    item.SetShifting(false);
                    item.SetChecked(item.ItemData.IsChecked);
                }

                menuView.UpdateMenuView();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Unable to set shift mode: {ex}");
            }
        }

        private void OnChildViewAdded(object sender, ViewGroup.ChildViewAddedEventArgs e)
        {
            if (!(e.Child is BottomNavigationItemView item))
                return;

            item.SetShifting(false);
            item.SetChecked(item.ItemData.IsChecked);
        }
    }
}
