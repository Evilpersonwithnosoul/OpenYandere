using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static OpenYandere.Managers.ObjectPoolManager;

namespace OpenYandere.Managers
{
    internal class ObjectPoolManager : MonoBehaviour
    {
        [System.Serializable]
        internal class PoolEntry
        {
            public string ObjectName;
            public GameObject PrefabObject;
            public GameObject ParentObject;
            public int PoolAmount;
            public bool AutomaticGrowth;

            [HideInInspector]
            public ObjectPool<GameObject> ObjectPooler;
        }

        public List<PoolEntry> PoolEntries = new List<PoolEntry>();
        public GameObject this[string name]
        {
            get
            {
                PoolEntry poolEntry = PoolEntries.Find(pooledObject => pooledObject.ObjectName == name);
                if (poolEntry == null || poolEntry.ObjectPooler == null) return null;

                GameObject obj = poolEntry.ObjectPooler.Get();

                if (obj)
                {
                    obj.transform.SetParent(poolEntry.ParentObject.transform);
                    obj.SetActive(true);
                }

                return obj;
            }
        }

        private void Awake()
        {
            foreach (var poolEntry in PoolEntries)
            {
                poolEntry.ObjectPooler = new ObjectPool<GameObject>(() => CreateObject(poolEntry), null, null, (obj) => DestroyObject(obj, poolEntry));

                // Populate initial objects
                for (int i = 0; i < poolEntry.PoolAmount; i++)
                {
                    GameObject obj = poolEntry.ObjectPooler.Get();
                    obj.SetActive(false);
                }
            }
        }

        private GameObject CreateObject(PoolEntry poolEntry)
        {
            if (poolEntry.PrefabObject == null)
            {
                Debug.LogErrorFormat("'{0}' has not been associated with a prefab.", poolEntry.ObjectName);
                return null;
            }

            var instantiatedObject = Instantiate(poolEntry.PrefabObject);
            instantiatedObject.SetActive(false);

            return instantiatedObject;
        }

        private void DestroyObject(GameObject obj, PoolEntry poolEntry)
        {
            Destroy(obj);
        }

        public void ReturnToPool(GameObject obj, string name)
        {
            PoolEntry poolEntry = PoolEntries.Find(pooledObject => pooledObject.ObjectName == name);
            if (poolEntry != null && poolEntry.ObjectPooler != null)
            {
                poolEntry.ObjectPooler.Release(obj);
            }
        }
    }
}
