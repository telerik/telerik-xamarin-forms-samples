using System;
using Xamarin.Forms;

namespace ArtGalleryCRM.Forms.Behaviors
{
    public class BehaviorBase<T> : Behavior<T> where T : BindableObject
    {
        public T AssociatedObject { get; private set; }

        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);

            AssociatedObject = bindable;

            if (bindable.BindingContext != null)
                this.BindingContext = bindable.BindingContext;

            bindable.BindingContextChanged += this.OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.BindingContextChanged -= this.OnBindingContextChanged;

            this.AssociatedObject = null;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            this.OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.BindingContext = this.AssociatedObject.BindingContext;
        }
    }
}