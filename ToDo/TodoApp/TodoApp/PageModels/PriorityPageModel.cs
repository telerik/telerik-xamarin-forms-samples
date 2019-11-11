using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TodoApp.Models;
using Xamarin.Forms;

namespace TodoApp.PageModels
{
    public class PriorityPageModel : FreshBasePageModelEx
    {
        public PriorityPageModel(Services.ITodoService todoService)
        {
            _service = todoService;
            _acceptCommand = new Command(OnSelectPriority);
        }

        private Services.ITodoService _service;
        private IReadOnlyCollection<Priority> _priorities;
        private Priority _selectedPriority;
        private Command _acceptCommand;

        public ICommand AcceptCommand => _acceptCommand;

        public Priority SelectedPriority
        {
            get => _selectedPriority;
            set => SetProperty(ref _selectedPriority, value);
        }

        public IReadOnlyCollection<Priority> Priorities
        {
            get => _priorities;
            private set => SetProperty(ref _priorities, value);
        }

        public override void Init(object initData)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (_priorities == null)
                {
                    Priorities = _service.GetPriorities();
                }
                if (initData is Priority selected)
                {
                    SelectedPriority = Priorities?.SingleOrDefault(c => c.ID == selected.ID);
                }
            });
        }

        private async void OnSelectPriority()
        {
            await CoreMethods.PopPageModel(_selectedPriority);
        }
    }
}
