using System.Collections.Generic;

namespace Core.Tools
{
    public interface IObjectPool<T>
    {
        T    Next();
        void Release(T released);
    }

    public class ObjectPool<T> : IObjectPool<T> where T : new()
    {
        public readonly List<T> free  = new List<T>();
        public readonly List<T> inUse = new List<T>();
        public readonly List<T> pool  = new List<T>();

        public T Next()
        {
            foreach (T free in free)
            {
                this.free.Remove(free);
                inUse.Add(free);

                return free;
            }

            T instanced = new T();
            pool.Add(instanced);
            inUse.Add(instanced);

            return instanced;
        }

        public void Release(T released)
        {
            free.Add(released);
            inUse.Remove(released);
        }

        public void Cull() => free.Clear();
    }
}