using System;

namespace ArtGalleryCRM.Forms.Models
{
    public class ChartDataPoint
    {
        // Used for Categorical series types
        public string Title { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }

        // Used for Scatter series types
        public double XValue { get; set; }
        public double YValue { get; set; }
    }
}