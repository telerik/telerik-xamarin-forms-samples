using System;

namespace ErpApp.Models
{
    public class PresentationContext<T>
    {
        public PresentationContext(T model, PresentationMode mode)
        {
            _model = model;
            _mode = mode;
        }

        private T _model;
        private PresentationMode _mode;

        public T Model => _model;
        public PresentationMode Mode => _mode;
    }

    public enum PresentationMode
    {
        Read,
        Create,
        Edit
    }
}
