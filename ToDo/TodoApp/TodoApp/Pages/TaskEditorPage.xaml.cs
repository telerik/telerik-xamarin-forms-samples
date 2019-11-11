using System;
using TodoApp.PageModels;
using Xamarin.Forms;

namespace TodoApp.Pages
{
    public partial class TaskEditorPage : ContentPage
	{
		public TaskEditorPage()
		{
			InitializeComponent();

            MessagingCenter.Subscribe<PageModels.TaskEditorPageModel>(this, PageModels.TaskEditorPageModel.OpenPopup_SelectAlert_Action, OnSelectAlert);
            MessagingCenter.Subscribe<PageModels.TaskEditorPageModel>(this, PageModels.TaskEditorPageModel.OpenPopup_SelectRecurrence_Action, OnSelectRecurrence);
            MessagingCenter.Subscribe<PageModels.TaskEditorPageModel>(this, PageModels.TaskEditorPageModel.ClosePopup_SelectAlert_Action, _ => this.popup.IsOpen = false);
            MessagingCenter.Subscribe<PageModels.TaskEditorPageModel>(this, PageModels.TaskEditorPageModel.ClosePopup_SelectRecurrence_Action, _ => this.popup.IsOpen = false);
        }

        private void OnSelectAlert(TaskEditorPageModel sender)
        {
            var popupWidth = Math.Min(this.Width * 0.9, 300);
            var popupHeight = Math.Min(this.Height * 0.8, 400);

            this.popup.Content = new Views.AlertSelectView()
            {
                BindingContext = sender.AlertSelectModel,
                WidthRequest = popupWidth,
                HeightRequest = popupHeight
            };
            this.popup.IsOpen = true;
        }

        private void OnSelectRecurrence(TaskEditorPageModel sender)
        {
            var popupWidth = Math.Min(this.Width * 0.9, 300);
            var popupHeight = Math.Min(this.Height * 0.8, 400);

            this.popup.Content = new Views.RecurrenceSelectView()
            {
                BindingContext = sender.RecurrenceSelectModel,
                WidthRequest = popupWidth,
                HeightRequest = popupHeight
            };
            this.popup.IsOpen = true;
        }
    }
}