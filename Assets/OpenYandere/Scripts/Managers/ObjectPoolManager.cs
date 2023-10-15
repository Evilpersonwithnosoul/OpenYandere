using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace OpenYandere.Managers
{
    internal class ObjectPoolManager : MonoBehaviour
    {
        [Serializable]
        internal class PoolEntry
        {
            [Tooltip("The name that will be used to access the pooled objects.")]
            public string ObjectName;
            [Tooltip("The prefab of the object to be pooled.")]
            public GameObject PrefabObject;
            [Tooltip("The parent that the prefab should be a child of.")]
            public GameObject ParentObject;
            [Tooltip("The number of objects to be pooled.")]
            public int PoolAmount;
            [Tooltip("Can the pool grow if there are not enough objects?")]
            public bool AutomaticGrowth;

            [HideInInspector]
            public List<GameObject> PooledObjects = new List<GameObject>();
        }
        
        public List<PoolEntry> PoolEntries = new List<PoolEntry>();
        private bool _isCoroutineActive;
        
        public GameObject this[string name]
        {
            get
            {
                PoolEntry poolEntry = PoolEntries.First(pooledObject => pooledObject.ObjectName == name);

                if (poolEntry == null) return null;
                
                foreach (var pooledObject in poolEntry.PooledObjects)
                {
                    if (!pooledObject.gameObject.activeInHierarchy)
                    {
                        return pooledObject.gameObject;
                    }
                }

                return !poolEntry.AutomaticGrowth ? null : CreateObject(poolEntry);
            }
        }
        
        private void Awake()
        {
            foreach (var poolEntry in PoolEntries)
            {
                for (var i = 0; i < poolEntry.PoolAmount; i++)
                {
                    CreateObject(poolEntry);
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

            var instantinatedObject = poolEntry.ParentObject != null ? Instantiate(poolEntry.PrefabObject, poolEntry.ParentObject.transform) : Instantiate(poolEntry.PrefabObject);
            instantinatedObject.SetActive(false);

            poolEntry.PooledObjects.Add(instantinatedObject);

            if (!_isCoroutineActive && poolEntry.PooledObjects.Count > poolEntry.PoolAmount)
            {
                StartCoroutine(ClearExcess());
                _isCoroutineActive = true;
            }

            return instantinatedObject;
        }
        
        private IEnumerator ClearExcess()
        {
            // Yield a few seconds before checking for the first time.
            // Otherwise it would register the first object that goes over the limit
            // then wait 60 seconds before registering any made on subsequent frames.
            yield return new WaitForSeconds(3f);

            while (true)
            {
                var excessObjects = 0;

                foreach (var poolEntry in PoolEntries)
                {
                    if (!poolEntry.AutomaticGrowth || poolEntry.PooledObjects.Count <= poolEntry.PoolAmount) continue;
                    
                    // Get the items that are over the allowed range.
                    var autoPooledObjects = poolEntry.PooledObjects.GetRange(poolEntry.PoolAmount, ((poolEntry.PooledObjects.Count - 1) - (poolEntry.PoolAmount - 1)));

                    // Add the number of extra objects on to the count
                    excessObjects += autoPooledObjects.Count;

                    // Remove the objects that aren't in use (disabled)
                    foreach (var pooledObject in autoPooledObjects)
                    {
                        // Skip the if object is active.
                        if (pooledObject.activeInHierarchy) continue;
                        
                        poolEntry.PooledObjects.Remove(pooledObject);
                        Destroy(pooledObject);

                        // Remove it from the excess count as it was removed.
                        excessObjects -= 1;
                    }
                }

                if (excessObjects == 0)
                {
                    _isCoroutineActive = false;
                    break;
                }

                yield return new WaitForSeconds(60f);
            }
        }
    }
}