using System;
using System.Threading.Tasks;
using MvvmCross.Exceptions;
using MvvmCross.Forms.Presenters;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace ErpApp
{
    public class AppPagePresenter : MvxFormsPagePresenter
    {
        public AppPagePresenter(IMvxFormsViewPresenter platformPresenter)
            : base(platformPresenter)
        { }

        public override void RegisterAttributeTypes()
        {
            base.RegisterAttributeTypes();

            AttributeTypesToActionsDictionary.Register<MvxCustomMasterDetailPagePresentationAttribute>(ShowMasterDetailPage, CloseMasterDetailPage);
        }

        public override async Task<bool> ShowMasterDetailPage(Type view, MvvmCross.Forms.Presenters.Attributes.MvxMasterDetailPagePresentationAttribute attribute, MvxViewModelRequest request)
        {
            var page = await CloseAndCreatePage(view, request, attribute);

            if (attribute.Position == MasterDetailPosition.Root)
            {
                if (page is MasterDetailPage masterDetailRoot)
                {
                    if (masterDetailRoot.Master == null)
                        masterDetailRoot.Master = CreateContentPage().Build(cp => cp.Title = !string.IsNullOrEmpty(attribute.Title) ? attribute.Title : nameof(MvxMasterDetailPage));
                    if (masterDetailRoot.Detail == null)
                        masterDetailRoot.Detail = CreateContentPage().Build(cp => cp.Title = !string.IsNullOrEmpty(attribute.Title) ? attribute.Title : nameof(MvxMasterDetailPage));

                    await PushOrReplacePage(FormsApplication.MainPage, page, attribute);
                }
                else
                    throw new MvxException($"A root page should be of type {nameof(MasterDetailPage)}");
            }
            else
            {
                MasterDetailPage masterDetailHost = null;
                if (attribute is MvxCustomMasterDetailPagePresentationAttribute attr && attr.MasterHostViewType != null)
                {
                    masterDetailHost = GetPageOfTypeByType(attr.MasterHostViewType) as MasterDetailPage;
                }
                if (masterDetailHost == null)
                {
                    masterDetailHost = GetPageOfType<MasterDetailPage>();
                }

                if (masterDetailHost == null)
                {
                    //Assume we have to create the host
                    masterDetailHost = CreateMasterDetailPage();

                    if (!string.IsNullOrWhiteSpace(attribute.Icon))
                    {
                        masterDetailHost.IconImageSource = attribute.Icon;
                    }

                    masterDetailHost.Master = CreateContentPage().Build(cp => cp.Title = !string.IsNullOrWhiteSpace(attribute.Title) ? attribute.Title : nameof(MvxMasterDetailPage));
                    masterDetailHost.Detail = CreateContentPage();

                    var masterDetailRootAttribute = new MvxMasterDetailPagePresentationAttribute { Position = MasterDetailPosition.Root, WrapInNavigationPage = attribute.WrapInNavigationPage, NoHistory = attribute.NoHistory };

                    await PushOrReplacePage(FormsApplication.MainPage, masterDetailHost, masterDetailRootAttribute);
                }

                if (attribute.Position == MasterDetailPosition.Master)
                {
                    await PushOrReplacePage(masterDetailHost.Master, page, attribute);
                }
                else
                {
                    await PushOrReplacePage(masterDetailHost.Detail, page, attribute);
                }
            }
            return true;
        }

        public override TPage GetPageOfType<TPage>(Page rootPage = null)
        {
            if (rootPage == null)
                rootPage = FormsApplication.MainPage;

            if (rootPage is TPage)
                return rootPage as TPage;
            else if (rootPage is NavigationPage navigationRootPage)
            {
                if (navigationRootPage.CurrentPage is NavigationPage navigationNestedPage)
                    return GetPageOfType<TPage>(navigationNestedPage);
                else
                    return GetPageOfType<TPage>(navigationRootPage.CurrentPage);
            }
            else if (rootPage is MasterDetailPage masterDetailRoot)
            {
                var detailHost = GetPageOfType<TPage>(masterDetailRoot.Detail);
                if (detailHost is TPage)
                    return detailHost;
                else
                    return GetPageOfType<TPage>(masterDetailRoot.Master);
            }
            else if (rootPage is CarouselPage carouselPage)
            {
                foreach (var item in carouselPage.Children)
                {
                    var nestedPage = GetPageOfType<TPage>(item);
                    if (nestedPage is TPage)
                        return nestedPage;
                }
                return rootPage as TPage;
            }
            else if (rootPage is TabbedPage tabbedPage)
            {
                var nestedPage = GetPageOfType<TPage>(tabbedPage.CurrentPage);
                if (nestedPage is TPage)
                    return nestedPage;

                foreach (var item in tabbedPage.Children)
                {
                    nestedPage = GetPageOfType<TPage>(item);
                    if (nestedPage is TPage)
                        return nestedPage;
                }
                return rootPage as TPage;
            }
            else
                return rootPage as TPage;
        }

        public override async Task<bool> ShowTabbedPage(Type view, MvxTabbedPagePresentationAttribute attribute, MvxViewModelRequest request)
        {
            var page = await CloseAndCreatePage(view, request, attribute);

            if (attribute.Position == TabbedPosition.Root)
            {
                if (page is TabbedPage tabbedPageRoot)
                {
                    await PushOrReplacePage(FormsApplication.MainPage, page, attribute);
                }
                else
                    throw new MvxException($"A root page should be of type {nameof(TabbedPage)}");
            }
            else
            {
                var tabHost = GetPageOfType<TabbedPage>();
                if (tabHost == null)
                {
                    tabHost = new TabbedPage();
                    await PushOrReplacePage(FormsApplication.MainPage, tabHost, attribute);
                }

                if (attribute.WrapInNavigationPage)
                {
                    page = CreateNavigationPage(page).Build(tp =>
                    {
                        tp.Title = page.Title;
                        tp.IconImageSource = page.IconImageSource;
                    });
                }

                tabHost.Children.Add(page);
            }
            return true;
        }
    }
}
