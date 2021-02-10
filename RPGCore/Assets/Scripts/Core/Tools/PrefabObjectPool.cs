using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Tools
{
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
            if (0 < free.Count)
            {
                T available = free[0];

                free.Remove(available);
                available.gameObject.SetActive(true);

                return available;
            }

            T instanced = Object.Instantiate(prefab, parent);
            pool.Add(instanced);
            inUse.Add(instanced);

            return instanced;
        }

        public void Release(T released)
        {
            released.gameObject.SetActive(false);

            if (inUse.Contains(released))
                inUse.Remove(released);

            free.Add(released);
        }

        public void Cull()
        {
            foreach (T clutter in free)
            {
                pool.Remove(clutter);
                Object.Destroy(clutter.gameObject);
            }

            free.Clear();
        }

        public void ReleaseAll()
        {
            foreach (T element in pool.Where(s => !free.Contains(s)))
                Release(element);

            inUse.Clear();
        }
    }

    public class ElegantPrefabObjectPool<T> : IObjectPool<T> where T : MonoBehaviour
    {
        private readonly Transform parent;
        public readonly  List<T>   pool = new List<T>();

        private readonly T prefab;

        public ElegantPrefabObjectPool(T prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
        }

        public IEnumerable<T> Free  => pool.Where(e => !e.gameObject.activeSelf);
        public IEnumerable<T> InUse => pool.Where(e => e.gameObject.activeSelf);

        public T Next()
        {
            foreach (T candidate in Free)
            {
                candidate.gameObject.SetActive(true);

                return candidate;
            }

            T instanced = Object.Instantiate(prefab, parent);
            pool.Add(instanced);

            return instanced;
        }

        public void Release(T released) => released.gameObject.SetActive(false);

        public void Cull()
        {
            foreach (T clutter in Free)
            {
                pool.Remove(clutter);
                Object.Destroy(clutter.gameObject);
            }
        }

        public void ReleaseAll()
        {
            foreach (T element in InUse)
                Release(element);
        }
    }
}