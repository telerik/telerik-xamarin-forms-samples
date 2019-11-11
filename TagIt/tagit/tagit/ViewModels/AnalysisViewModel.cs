using System;
using System.Collections.ObjectModel;
using System.Linq;
using tagit.Common;
using tagit.Models;

namespace tagit.ViewModels
{
    /// <summary>
    /// View model to serve the Analysis view
    /// </summary>
    public class AnalysisViewModel : ObservableBase
    {
        private CategoryAnalysisInformation _currentCategoryAnalysis = new CategoryAnalysisInformation();

        private ObservableCollection<TagAnalysisInformation> _tagBreakdown =
            new ObservableCollection<TagAnalysisInformation>();

        public AnalysisViewModel()
        {
            if (App.ViewModel != null) RefreshAnalysis();
        }

        public ObservableCollection<TagAnalysisInformation> TagBreakdown
        {
            get => _tagBreakdown;
            set => SetProperty(ref _tagBreakdown, value);
        }

        public CategoryAnalysisInformation CurrentCategoryAnalysis
        {
            get => _currentCategoryAnalysis;
            set => SetProperty(ref _currentCategoryAnalysis, value);
        }

        internal void RefreshAnalysis()
        {
            var allCategories = App.ViewModel.AllCategories.ToList();
            var allTags = App.ViewModel.AllTags.ToList();

            if (allCategories.Count > 3)
            {
                var categoryAnalysis = allCategories.GroupBy(n => n,
                        (key, values) => new {Category = key, Count = values.Count()}).OrderByDescending(o => o.Count)
                    .ToList();

                if (categoryAnalysis.Count > 0)
                {
                    CurrentCategoryAnalysis.Tag1Label = categoryAnalysis[0].Category.Trim().ToUpper();
                    CurrentCategoryAnalysis.Tag1Value =
                        Math.Round((double) categoryAnalysis[0].Count / categoryAnalysis.Count, 1);
                }

                if (categoryAnalysis.Count > 1)
                {
                    CurrentCategoryAnalysis.Tag2Label = categoryAnalysis[1].Category.Trim().ToUpper();
                    CurrentCategoryAnalysis.Tag2Value =
                        Math.Round((double) categoryAnalysis[1].Count / categoryAnalysis.Count, 1);
                }

                if (categoryAnalysis.Count > 2)
                {
                    CurrentCategoryAnalysis.Tag3Label = categoryAnalysis[2].Category.Trim().ToUpper();
                    CurrentCategoryAnalysis.Tag3Value =
                        Math.Round((double) categoryAnalysis[2].Count / categoryAnalysis.Count, 1);
                }
            }

            if (allTags.Count > 3)
            {
                var tagAnalysis = allTags.GroupBy(n => n,
                    (key, values) => new {Tag = key, Count = values.Count()}).OrderByDescending(o => o.Count).ToList();

                TagBreakdown.Clear();

                var breakdown = (from item in tagAnalysis.Take(5)
                    select new TagAnalysisInformation
                    {
                        Label = item.Tag,
                        Value = item.Count
                    }).ToList();

                foreach (var item in breakdown)
                    TagBreakdown.Add(item);
            }
        }
    }
}