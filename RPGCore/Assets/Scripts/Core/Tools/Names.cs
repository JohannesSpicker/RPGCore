using System.Collections.Generic;
using System.Linq;

namespace Core.Tools
{
    public class Names<T>
    {
        public T bed;
        public T cupboard;
        public T table;
        public T window;

        public List<T> GetAllValues() => GetType().GetFields().Select(t => (T) t.GetValue(this)).ToList();
    }
}