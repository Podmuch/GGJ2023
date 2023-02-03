using System.Collections.Generic;
using UnityEngine;

namespace BoxColliders.Game
{
    public sealed class GameplayObjectsPool : MonoBehaviour
    {
        public GameObject Root;

        private Dictionary<string, Stack<GameObject>> objectsPool = new Dictionary<string, Stack<GameObject>>();

        public void Push(GameObject returnedObject, string prefabName)
        {
            returnedObject.SetActive(false);
            if (!objectsPool.ContainsKey(prefabName))
            {
                objectsPool.Add(prefabName, new Stack<GameObject>());
            }
            objectsPool[prefabName].Push(returnedObject);
        }

        public GameObject Pop(string prefabName)
        {
            if (objectsPool.ContainsKey(prefabName) &&
                objectsPool[prefabName].Count > 0)
            {
                var instance = objectsPool[prefabName].Pop();
                if (instance != null)
                {
                    instance.SetActive(true);
                    return instance;
                }
            }
            return Create(prefabName);
        }
        
        public void Preload(string prefabName)
        {
            var preloadedInstance = Create(prefabName);
            Push(preloadedInstance, prefabName);
        }

        public void Clear()
        {
            foreach (var entry in objectsPool)
            {
                while (entry.Value.Count > 0)
                {
                    var instance = entry.Value.Pop();
                    GameObject.Destroy(instance);
                }
            }
            objectsPool.Clear();
            GameObject.Destroy(Root);
        }

        private GameObject Create(string prefabName)
        {
            var prefab = Resources.Load<GameObject>(prefabName);
            var instance = GameObject.Instantiate(prefab, Root.transform);
            ResetTransform(instance.transform);
            return instance;
        }

        private void ResetTransform(Transform objTransform)
        {
            objTransform.position = Vector3.zero;
            objTransform.localScale = Vector3.one;
            objTransform.localRotation = Quaternion.identity;
        }
    }
}