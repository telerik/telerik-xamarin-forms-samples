using System.Collections.Generic;

namespace ErpApp.Services
{
    public interface IContextService
    {
        void Store(string identifier, object value);
        bool TryGet(string identifier, out object value);
        void Remove(string identifier);
    }

    public class ContextService : IContextService
    {
        public ContextService()
        {
            this.store = new Dictionary<string, object>();
        }

        private Dictionary<string, object> store;

        public bool TryGet(string identifier, out object value)
        {
            return this.store.TryGetValue(identifier, out value);
        }

        public void Store(string identifier, object value)
        {
            this.store.Remove(identifier);
            this.store.Add(identifier, value);
        }

        public void Remove(string identifier)
        {
            this.store.Remove(identifier);
        }
    }
}
