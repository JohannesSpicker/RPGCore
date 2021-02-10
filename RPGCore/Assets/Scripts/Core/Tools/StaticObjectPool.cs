using System;
using System.Collections.Generic;

namespace Core.Tools
{
    public interface IObjectPool<T>
    {
        T    Next();
        void Release(T released);
    }

    public static class StaticObjectPool<T> where T : new()
    {
        private static readonly List<T> s_pool = new List<T>();

        private static readonly List<T> s_freePool = new List<T>();

        public static T Next()
        {
            foreach (T free in s_freePool)
            {
                s_freePool.Remove(free);

                return free;
            }

            T instanced = new T();
            s_pool.Add(instanced);

            return instanced;
        }

        public static void Release(T released) => s_freePool.Add(released);
        public static void Cull()              => s_freePool.Clear();
    }

    public class LocalObjectPool<T> : IObjectPool<T> where T : new()
    {
        public T    Next()              => new T();
        public void Release(T released) => throw new NotImplementedException();
    }
}