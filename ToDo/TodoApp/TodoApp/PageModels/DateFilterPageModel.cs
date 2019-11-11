using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TodoApp.Models;
using TodoApp.Services;
using Xamarin.Forms;

namespace TodoApp.PageModels
{
    public class DateFilterPageModel : FreshBasePageModelEx
    {
        public DateFilterPageModel(ITodoService todoService)
        {
            _todoService = todoService;
            _acceptCommand = new Command(OnAccept);
            _filters = new DateFilter[]
            {
                new OverdueDateFilter(),
                new TomorrowDateFilter(),
                new NextWeekDateFilter(),
                new ForthcomingDateFilter(),
                new EmptyDateFilter(),
                new TodayDateFilter()
            };
        }

        private IReadOnlyCollection<FilterResults> _filterResults;
        private ITodoService _todoService;
        private DateFilter[] _filters;
        private FilterResults _selectedFilter;
        private Command _acceptCommand;

        public IReadOnlyCollection<FilterResults> Filters
        {
            get => _filterResults;
            private set => SetProperty(ref _filterResults, value);
        }

        public FilterResults SelectedFilter
        {
            get => _selectedFilter;
            set => SetProperty(ref _selectedFilter, value);
        }

        public ICommand AcceptCommand => _acceptCommand;

        public override void Init(object initData)
        {
            var items = _todoService.GetTodoItems();

            Filters = _filters.Select(c => c.Filter(items)).ToArray();
        }

        private async void OnAccept()
        {
            await this.CoreMethods.PopPageModel(_selectedFilter?.Filter);
        }
    }
}
