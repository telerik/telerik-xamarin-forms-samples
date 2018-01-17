using System;
using Xamarin.Forms;

namespace QSF.Layouts
{
    public class WrapLayout : Layout<View>
    {
        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create("Orientation", typeof(StackOrientation), typeof(WrapLayout), StackOrientation.Horizontal,
                propertyChanged: (bindable, oldvalue, newvalue) => ((WrapLayout)bindable).ForceLayout());

        public static readonly BindableProperty SpacingProperty =
            BindableProperty.Create("Spacing", typeof(double), typeof(WrapLayout), 6.0,
                propertyChanged: (bindable, oldvalue, newvalue) => ((WrapLayout)bindable).ForceLayout());

        public StackOrientation Orientation
        {
            get
            {
                return (StackOrientation)this.GetValue(OrientationProperty);
            }
            set
            {
                this.SetValue(OrientationProperty, value);
            }
        }

        public double Spacing
        {
            get
            {
                return (double)this.GetValue(SpacingProperty);
            }
            set
            {
                this.SetValue(SpacingProperty, value);
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (this.WidthRequest > 0)
            {
                widthConstraint = Math.Min(widthConstraint, this.WidthRequest);
            }

            if (this.HeightRequest > 0)
            {
                heightConstraint = Math.Min(heightConstraint, this.HeightRequest);
            }

            if (!double.IsPositiveInfinity(widthConstraint))
            {
                widthConstraint = Math.Max(0, widthConstraint);
            }

            if (!double.IsPositiveInfinity(heightConstraint))
            {
                heightConstraint = Math.Max(0, heightConstraint);
            }

            if (this.Orientation == StackOrientation.Vertical)
            {
                return this.MeasureVertical(widthConstraint, heightConstraint);
            }
            else
            {
                return this.MeasureHorizontal(widthConstraint, heightConstraint);
            }
        }

        private SizeRequest MeasureVertical(double widthConstraint, double heightConstraint)
        {
            var columnCount = 1;
            var width = 0.0;
            var height = 0.0;
            var minWidth = 0.0;
            var minHeight = 0.0;
            var heightUsed = 0.0;

            foreach (var child in this.Children)
            {
                if (child.IsVisible)
                {
                    var size = child.Measure(widthConstraint, heightConstraint);

                    width = Math.Max(width, size.Request.Width);

                    var newHeight = height + size.Request.Height + this.Spacing;

                    if (newHeight > heightConstraint)
                    {
                        columnCount++;
                        heightUsed = Math.Max(height, heightUsed);
                        height = size.Request.Height;
                    }
                    else
                    {
                        height = newHeight;
                    }

                    minHeight = Math.Max(minHeight, size.Minimum.Height);
                    minWidth = Math.Max(minWidth, size.Minimum.Width);
                }
            }

            if (columnCount > 1)
            {
                height = Math.Max(height, heightUsed);
                width *= columnCount;
            }

            var request = new Size(width, height);
            var minimum = new Size(minWidth, minHeight);

            return new SizeRequest(request, minimum);
        }

        private SizeRequest MeasureHorizontal(double widthConstraint, double heightConstraint)
        {
            var rowCount = 1;
            var width = 0.0;
            var height = 0.0;
            var minWidth = 0.0;
            var minHeight = 0.0;
            var widthUsed = 0.0;

            foreach (var child in this.Children)
            {
                if (child.IsVisible)
                {
                    var size = child.Measure(widthConstraint, heightConstraint);

                    height = Math.Max(height, size.Request.Height);

                    var newWidth = width + size.Request.Width + this.Spacing;

                    if (newWidth > widthConstraint)
                    {
                        rowCount++;
                        widthUsed = Math.Max(width, widthUsed);
                        width = size.Request.Width;
                    }
                    else
                    {
                        width = newWidth;
                    }

                    minHeight = Math.Max(minHeight, size.Minimum.Height);
                    minWidth = Math.Max(minWidth, size.Minimum.Width);
                }
            }

            if (rowCount > 1)
            {
                width = Math.Max(width, widthUsed);
                height = (height + this.Spacing) * rowCount;
            }

            var request = new Size(width, height);
            var minimum = new Size(minWidth, minHeight);

            return new SizeRequest(request, minimum);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            if (this.Orientation == StackOrientation.Vertical)
            {
                this.LayoutVertical(x, y, width, height);
            }
            else
            {
                this.LayoutHorizontal(x, y, width, height);
            }
        }

        private void LayoutVertical(double x, double y, double width, double height)
        {
            var columnWidth = 0.0;
            var childX = x;
            var childY = y;

            foreach (var child in this.Children)
            {
                if (child.IsVisible)
                {
                    var request = child.Measure(width, height);

                    var childWidth = request.Request.Width;
                    var childHeight = request.Request.Height;

                    columnWidth = Math.Max(columnWidth, childWidth);

                    if (childY + childHeight > height)
                    {
                        childY = y;
                        childX += columnWidth + this.Spacing;
                        columnWidth = 0;
                    }

                    var region = new Rectangle(childX, childY, childWidth, childHeight);

                    LayoutChildIntoBoundingRegion(child, region);

                    childY += region.Height + this.Spacing;
                }
            }
        }

        private void LayoutHorizontal(double x, double y, double width, double height)
        {
            var rowHeight = 0.0;
            var childX = x;
            var childY = y;

            foreach (var child in this.Children)
            {
                if (child.IsVisible)
                {
                    var request = child.Measure(width, height);

                    var childWidth = request.Request.Width;
                    var childHeight = request.Request.Height;

                    rowHeight = Math.Max(rowHeight, childHeight);

                    if (childX + childWidth > width)
                    {
                        childX = x;
                        childY += rowHeight + this.Spacing;
                        rowHeight = 0;
                    }

                    var region = new Rectangle(childX, childY, childWidth, childHeight);

                    LayoutChildIntoBoundingRegion(child, region);

                    childX += region.Width + this.Spacing;
                }
            }
        }
    }
}
