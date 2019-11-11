using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.XamarinForms.Common;
using Xamarin.Forms;

namespace TodoApp.Models
{
    public class Category : BindableBase
    {
        public Category(int id, string name, Color color, IEnumerable<TodoItem> items)
        {
            _id = id;
            _name = name;
            _color = color;
            _brush = new RadSolidColorBrush(color);
            _items = items != null ? new ObservableCollection<TodoItem>(items) : new ObservableCollection<TodoItem>();
            _items.CollectionChanged += (s, e) => RaisePropertyChanged(nameof(PendingItemsCount));
            _isValid = CheckValid();
        }

        public Category()
        {
        }

        private int _id, _index;
        private bool _isValid;
        private string _name;
        private Color _color;
        private RadBrush _brush;
        private ObservableCollection<TodoItem> _items;

        public bool IsNew => _id <= 0;

        public int ID => _id;

        public bool IsValid
        {
            get => _isValid;
            private set => SetProperty(ref _isValid, value);
        }

        public int Index
        {
            get => _index;
            set => SetProperty(ref _index, value);
        }

        public string Name
        {
            get => _name;
            set
            {
                if (SetProperty(ref _name, value))
                {
                    IsValid = CheckValid();
                }
            }
        }

        public Color Color
        {
            get => _color;
            set
            {
                if (SetProperty(ref _color, value))
                {
                    Brush = new RadSolidColorBrush(value);
                }
            }
        }

        public RadBrush Brush
        {
            get => _brush;
            private set => SetProperty(ref _brush, value);
        }

        public ObservableCollection<TodoItem> Items
        {
            get => _items;
            private set => SetProperty(ref _items, value);
        }

        public int PendingItemsCount => _items?.Where(c => !c.Completed).Count() ?? 0;

        private bool CheckValid() => !string.IsNullOrEmpty(this.Name);

        public class IDEqualityComparer : IEqualityComparer<Category>
        {
            static IDEqualityComparer()
            {
                _instance = new Lazy<IDEqualityComparer>();
            }

            private static Lazy<IDEqualityComparer> _instance;

            public static IDEqualityComparer Instance => _instance.Value;

            public bool Equals(Category x, Category y)
            {
                return int.Equals(x?.ID, y?.ID);
            }

            public int GetHashCode(Category obj)
            {
                return obj?.ID.GetHashCode() ?? -1;
            }
        }

        public class NameEqualityComparer : IEqualityComparer<Category>
        {
            static NameEqualityComparer()
            {
                _instance = new Lazy<NameEqualityComparer>();
            }

            private static Lazy<NameEqualityComparer> _instance;

            public static NameEqualityComparer Instance => _instance.Value;

            public bool Equals(Category x, Category y)
            {
                return string.Equals(x?.Name, y?.Name);
            }

            public int GetHashCode(Category obj)
            {
                return obj?.Name?.GetHashCode() ?? -1;
            }
        }
    }
}
