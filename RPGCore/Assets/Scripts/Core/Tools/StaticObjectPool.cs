using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

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

    public class PrefabObjectPool<T> : IObjectPool<T> where T : MonoBehaviour
    {
        public readonly List<T> free  = new List<T>();
        public readonly List<T> inUse = new List<T>();

        private readonly Transform parent;
        public readonly  List<T>   pool = new List<T>();

        private readonly T prefab;

        public PrefabObjectPool(T prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
        }

        public T Next()
        {
            foreach (T free in free)
            {
                this.free.Remove(free);
                free.gameObject.SetActive(true);

                return free;
            }

            T instanced = Object.Instantiate(prefab, parent);
            pool.Add(instanced);
            inUse.Add(instanced);

            return instanced;
        }

        public void Release(T released)
        {
            released.gameObject.SetActive(false);
            inUse.Remove(released);
            free.Add(released);
        }

        public void Cull()
        {
            foreach (T free in free)
                pool.Remove(free);

            free.Clear();
        }

        public void ReleaseAll()
        {
            foreach (T element in pool.Where(s => !free.Contains(s)))
                Release(element);
            
            inUse.Clear();
        }
    }
}