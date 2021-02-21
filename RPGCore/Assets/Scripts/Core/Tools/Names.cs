using System.Collections.Generic;
using System.Linq;

namespace Core.Tools
{
    public class Names<T>
    {
        public T     bed;
        public T     cupboard;
        public short someShort;

        public string someString;
        public uint   someUint;
        public T      table;
        public T      window;

        public IEnumerable<T> GetAllValues() => GetType().GetFields().Where(fi => fi.FieldType == typeof(T))
                                                         .Select(t => (T) t.GetValue(this));

        //trying to incorporate arrays:
        /*
        public IEnumerable<T> GetAllValues() => GetType().GetFields().Where(fi => fi.FieldType == typeof(T))
                                                                .Select(t => (T) t.GetValue(this)).Union(chairs).Union(unnamedSlots);
        */
    }
}