using System.Collections.Generic;
using System.Linq;
using ArtGalleryCRM.Forms.Interfaces;
using ArtGalleryCRM.Forms.Models;
using ArtGalleryCRM.Forms.ViewModels;
using ArtGalleryCRM.Forms.ViewModels.EmployeeViewModels;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.Primitives;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.Views.EmployeePages
{
    public partial class EmployeeDetailPage : IGaugesView
    {
        public EmployeeDetailPage()
        {
            this.InitializeComponent();
        }
        
        public EmployeeDetailPage(EmployeeDetailViewModel viewModel)
        {
            this.InitializeComponent();
            this.BindingContext = viewModel;
            viewModel.GaugesView = this;
        }

        public void ConfigureGauges()
        {
            if (this.BindingContext is EmployeeDetailViewModel vm)
            {
                // Vacation Radial Gauge
                this.VacationLinearAxis.Maximum = vm.SelectedEmployee.VacationBalance;
                this.VacationRange.To = vm.SelectedEmployee.VacationBalance;
                this.VacationIndicator.Value = vm.SelectedEmployee.VacationUsed;

                // Sales Horizontal Gauge
                this.SalesLinearAxis.Maximum = vm.CompanySalesCount;
                this.SalesLinearAxis.Step = (double)vm.CompanySalesCount / 4;
                this.CompanySalesRange.To = vm.CompanySalesCount;
                this.EmployeeSalesIndicator.Value = vm.EmployeeSalesCount;

                // Revenue Horizontal Gauge
                this.RevenueLinearAxis.Maximum = vm.CompanySalesRevenue;
                this.RevenueLinearAxis.Step = vm.CompanySalesRevenue / 4;
                this.CompanyRevenueRange.To = vm.CompanySalesRevenue;
                this.EmployeeRevenueIndicator.Value = vm.EmployeeSalesRevenue;


                // Dynamically create a custom pie series using RadPath instead of RadPieChart
                var pieChartWithLegend = GeneratePieChart(vm.CompensationData, new List<Color>
                {
                    (Color) Application.Current.Resources["AccentTertiaryColor"],
                    (Color) Application.Current.Resources["AccentSecondaryColor"],
                    (Color) Application.Current.Resources["AccentLightColor"]
                });

                PieChartGrid.Children.Add(pieChartWithLegend);
            }
        }

        private static Grid GeneratePieChart(IList<ChartDataPoint> dataPoints, List<Color> colors, bool showLegend = true)
        {
            // Root container to hold the chart and any legend
            var container = new Grid();
            container.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
            container.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            // ***** Part 1 - PIE SERIES GENERATION ***** //

            // Sum up all the values to be displayed
            var totalValue = dataPoints.Sum(d => d.Value);

            // Variable to keep track of where each slice ended.
            double currentPosition = 0;

            // Iterate over the data points to create slices.
            for (int i = 0; i < dataPoints.Count; i++)
            {
                // Determine the percentage of the whole the slice uses.
                double slicePercentage = dataPoints[i].Value / totalValue;

                // Calculate the sweep using that percentage
                double sweep = slicePercentage * 360;

                // Create the ArcSegment using the calculated values.
                var segment = new RadArcSegment
                {
                    Center = new Point(0.5, 0.5),
                    Size = new Size(1, 1),
                    StartAngle = currentPosition,
                    SweepAngle = sweep,
                };

                // Important - Calculate the last segment's ending angle in order to have a valid start angle for the next loop.
                currentPosition = currentPosition + sweep - 360;

                // Prepare the required PathFigure and add the ArcSegment
                var figure = new RadPathFigure { StartPoint = new Point(0.5,0.5) };
                figure.Segments.Add(segment);

                // Create the PathGeometry and add the PathFigure
                var geometry = new RadPathGeometry();
                geometry.Figures.Add(figure);

                // Construct the RadPath
                // - Select a Fill color from the brushes parameter (important: use a modulus to wrap to the beginning)
                // - Use the Geometry created from the value
                var slice = new RadPath
                {
                    Fill = new RadSolidColorBrush(colors[i % colors.Count]),
                    Geometry = geometry,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 100,
                    HeightRequest = 100,
                    Margin = new Thickness(0, 20, 0, 0)
                };

                // This isn't necessary, but added for completion.
                Grid.SetRow(slice, 0);

                // Finally, add it to the container.
                container.Children.Add(slice);
            }

            // ***** Part 2 - LEGEND GENERATION ***** //

            if (showLegend)
            {
                var legendPanel = new StackLayout();
                legendPanel.Orientation = StackOrientation.Horizontal;
                legendPanel.HorizontalOptions = LayoutOptions.Center;
                legendPanel.VerticalOptions = LayoutOptions.Center;
                legendPanel.Margin = new Thickness(0,16,0,0);
                legendPanel.Spacing = 5;

                for (int i = 0; i < dataPoints.Count; i++)
                {
                    // Use a RadBorder with only a bottom thickness and match the color to the slice
                    var legendItem = new RadBorder();
                    legendItem.BorderColor = colors[i % colors.Count];
                    legendItem.BorderThickness = new Thickness(0, 0, 0, 2);

                    // Create a Label for each data point and use the Title property
                    var label = new Label
                    {
                        Text = dataPoints[i].Title,
                        FontSize = 12,
                        Margin = new Thickness(0, 0, 0, 2),
                        TextColor = (Color) Application.Current.Resources["LightGrayTextColor"]
                    };

                    legendItem.Content = label;

                    legendPanel.Children.Add(legendItem);
                }

                Grid.SetRow(legendPanel, 1);

                container.Children.Add(legendPanel);
            }

            return container;
        }
    }
}