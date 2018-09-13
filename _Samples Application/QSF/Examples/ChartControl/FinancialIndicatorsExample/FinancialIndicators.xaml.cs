using System;
using Telerik.XamarinForms.Chart;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QSF.Examples.ChartControl.FinancialIndicatorsExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinancialIndicatorsView : ContentView
    {
        public FinancialIndicatorsView()
        {
            InitializeComponent();
            this.BindingContext = new FinancialIndicatorsViewModel();

            this.trendlinePicker.SelectedIndex = 0;
            this.indicatorPicker.SelectedIndex = 0;
        }

        private void OnIndicatorPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            this.indicatorsChart.Series.Clear();

            IndicatorBase indicatorLineToAdd = CreateIndicator((Indicators)indicatorPicker.SelectedItem);
            this.indicatorsChart.Series.Add(indicatorLineToAdd);

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.indicatorsChart.VerticalAxis = new NumericalAxis() { LineColor = Color.FromHex("#A9A9A9"), MajorTickBackgroundColor = Color.FromHex("#A9A9A9") };
            }
        }

        private void OnTrendlinePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var series = this.trendlinesChart.Series;
            if (series.Count > 1)
            {
                series.RemoveAt(series.Count - 1);
            }

            IndicatorBase trendlineToAdd = CreateTrendline((Trendlines)trendlinePicker.SelectedItem);
            this.trendlinesChart.Series.Add(trendlineToAdd);
        }

        private IndicatorBase CreateTrendline(Trendlines trendlineToAdd)
        {
            switch (trendlineToAdd)
            {
                case Trendlines.MovingAverageIndicator:
                    var maIndicator = new MovingAverageIndicator();
                    maIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    maIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    maIndicator.ValueBinding = new PropertyNameDataPointBinding("Close");
                    maIndicator.Period = 12;
                    maIndicator.Stroke = Color.Orange;
                    maIndicator.StrokeThickness = 1;
                    return maIndicator;

                case Trendlines.AdaptiveMovingAverageKaufmanIndicator:
                    var amakIndicator = new AdaptiveMovingAverageKaufmanIndicator();
                    amakIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    amakIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    amakIndicator.ValueBinding = new PropertyNameDataPointBinding("Close");
                    amakIndicator.Period = 10;
                    amakIndicator.SlowPeriod = 30;
                    amakIndicator.FastPeriod = 2;
                    amakIndicator.Stroke = Color.Orange;
                    amakIndicator.StrokeThickness = 1;
                    return amakIndicator;

                case Trendlines.ExponentialMovingAverageIndicator:
                    var emaIndicator = new ExponentialMovingAverageIndicator();
                    emaIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    emaIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    emaIndicator.ValueBinding = new PropertyNameDataPointBinding("Close");
                    emaIndicator.Period = 8;
                    emaIndicator.Stroke = Color.Orange;
                    emaIndicator.StrokeThickness = 1;
                    return emaIndicator;

                case Trendlines.BollingerBandsIndicator:
                    var bbIndicator = new BollingerBandsIndicator();
                    bbIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    bbIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    bbIndicator.ValueBinding = new PropertyNameDataPointBinding("Close");
                    bbIndicator.Period = 5;
                    bbIndicator.Stroke = Color.Orange;
                    bbIndicator.LowerBandStroke = Color.Orange;
                    bbIndicator.StandardDeviations = 3;
                    bbIndicator.StrokeThickness = 1;
                    return bbIndicator;

                case Trendlines.WeightedMovingAverageIndicator:
                    var wmaIndicator = new WeightedMovingAverageIndicator();
                    wmaIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    wmaIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    wmaIndicator.ValueBinding = new PropertyNameDataPointBinding("Close");
                    wmaIndicator.Period = 12;
                    wmaIndicator.Stroke = Color.Orange;
                    wmaIndicator.StrokeThickness = 1;
                    return wmaIndicator;

                default:
                    return null;
            }
        }

        private IndicatorBase CreateIndicator(Indicators indicatorToAdd)
        {
            switch (indicatorToAdd)
            {
                case Indicators.CommodityChannelIndexIndicator:
                    var cciIndicator = new CommodityChannelIndexIndicator();
                    cciIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    cciIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    cciIndicator.LowBinding = new PropertyNameDataPointBinding("Low");
                    cciIndicator.HighBinding = new PropertyNameDataPointBinding("High");
                    cciIndicator.CloseBinding = new PropertyNameDataPointBinding("Close");
                    cciIndicator.Period = 10;
                    cciIndicator.Stroke = Color.Green;
                    cciIndicator.StrokeThickness = 1;
                    return cciIndicator;

                case Indicators.AverageTrueRangeIndicator:
                    var artIndicator = new AverageTrueRangeIndicator();
                    artIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    artIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    artIndicator.LowBinding = new PropertyNameDataPointBinding("Low");
                    artIndicator.HighBinding = new PropertyNameDataPointBinding("High");
                    artIndicator.CloseBinding = new PropertyNameDataPointBinding("Close");
                    artIndicator.Period = 14;
                    artIndicator.Stroke = Color.Green;
                    artIndicator.StrokeThickness = 1;
                    return artIndicator;

                case Indicators.TrueRangeIndicator:
                    var trIndicator = new TrueRangeIndicator();
                    trIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    trIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    trIndicator.LowBinding = new PropertyNameDataPointBinding("Low");
                    trIndicator.HighBinding = new PropertyNameDataPointBinding("High");
                    trIndicator.CloseBinding = new PropertyNameDataPointBinding("Close");
                    trIndicator.Stroke = Color.Green;
                    trIndicator.StrokeThickness = 1;
                    return trIndicator;

                case Indicators.MacdIndicator:
                    var macdIndicator = new MacdIndicator();
                    macdIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    macdIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    macdIndicator.ValueBinding = new PropertyNameDataPointBinding("Close");
                    macdIndicator.ShortPeriod = 2;
                    macdIndicator.SignalPeriod = 6;
                    macdIndicator.LongPeriod = 9;
                    macdIndicator.Stroke = Color.Green;
                    macdIndicator.SignalStroke = Color.Red;
                    macdIndicator.StrokeThickness = 1;
                    return macdIndicator;

                case Indicators.RelativeMomentumIndexIndicator:
                    var rmiIndicator = new RelativeMomentumIndexIndicator();
                    rmiIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    rmiIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    rmiIndicator.ValueBinding = new PropertyNameDataPointBinding("Close");
                    rmiIndicator.Stroke = Color.Green;
                    rmiIndicator.Period = 12;
                    rmiIndicator.StrokeThickness = 1;
                    return rmiIndicator;

                case Indicators.RateOfChangeIndicator:
                    var rocIndicator = new RateOfChangeIndicator();
                    rocIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    rocIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    rocIndicator.ValueBinding = new PropertyNameDataPointBinding("Close");
                    rocIndicator.Stroke = Color.Green;
                    rocIndicator.Period = 8;
                    rocIndicator.StrokeThickness = 1;
                    return rocIndicator;

                case Indicators.RelativeStrengthIndexIndicator:
                    var rsiIndicator = new RelativeStrengthIndexIndicator();
                    rsiIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    rsiIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    rsiIndicator.ValueBinding = new PropertyNameDataPointBinding("Close");
                    rsiIndicator.Stroke = Color.Green;
                    rsiIndicator.Period = 8;
                    rsiIndicator.StrokeThickness = 1;
                    return rsiIndicator;

                case Indicators.StochasticFastIndicator:
                    var sfIndicator = new StochasticFastIndicator();
                    sfIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    sfIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    sfIndicator.LowBinding = new PropertyNameDataPointBinding("Low");
                    sfIndicator.HighBinding = new PropertyNameDataPointBinding("High");
                    sfIndicator.CloseBinding = new PropertyNameDataPointBinding("Close");
                    sfIndicator.MainPeriod = 14;
                    sfIndicator.SignalPeriod = 3;
                    sfIndicator.SignalStroke = Color.Red;
                    sfIndicator.Stroke = Color.Green;
                    sfIndicator.StrokeThickness = 1;
                    return sfIndicator;

                case Indicators.StochasticSlowIndicator:
                    var ssIndicator = new StochasticSlowIndicator();
                    ssIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    ssIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    ssIndicator.LowBinding = new PropertyNameDataPointBinding("Low");
                    ssIndicator.HighBinding = new PropertyNameDataPointBinding("High");
                    ssIndicator.CloseBinding = new PropertyNameDataPointBinding("Close");
                    ssIndicator.MainPeriod = 14;
                    ssIndicator.SlowingPeriod = 2;
                    ssIndicator.SignalPeriod = 3;
                    ssIndicator.SignalStroke = Color.Red;
                    ssIndicator.Stroke = Color.Green;
                    ssIndicator.StrokeThickness = 1;
                    return ssIndicator;

                case Indicators.TrixIndicator:
                    var trixIndicator = new TrixIndicator();
                    trixIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    trixIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    trixIndicator.ValueBinding = new PropertyNameDataPointBinding("Close");
                    trixIndicator.Stroke = Color.Green;
                    trixIndicator.Period = 15;
                    trixIndicator.StrokeThickness = 1;
                    return trixIndicator;

                case Indicators.UltimateOscillatorIndicator:
                    var uoIndicator = new UltimateOscillatorIndicator();
                    uoIndicator.SetBinding(CommodityChannelIndexIndicator.ItemsSourceProperty, new Binding("SeriesData"));
                    uoIndicator.CategoryBinding = new PropertyNameDataPointBinding("DateCategory");
                    uoIndicator.LowBinding = new PropertyNameDataPointBinding("Low");
                    uoIndicator.HighBinding = new PropertyNameDataPointBinding("High");
                    uoIndicator.CloseBinding = new PropertyNameDataPointBinding("Close");
                    uoIndicator.Period = 5;
                    uoIndicator.Period2 = 8;
                    uoIndicator.Period3 = 10;
                    uoIndicator.Stroke = Color.Green;
                    uoIndicator.StrokeThickness = 1;
                    return uoIndicator;

                default:
                    return null;
            }
        }
    }
}