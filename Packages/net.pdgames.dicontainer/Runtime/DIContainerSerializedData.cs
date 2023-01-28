using System;
using System.Collections.Generic;

namespace PDGames.DIContainer
{
    [Serializable]
    public sealed class DIContainerSerializedData
    {
        [Serializable]
        public struct ObjectSerializedData
        {
            public string context;
            public List<string> references;
        }

        public int objectsCount;
        public List<ObjectSerializedData> objectsData = new List<ObjectSerializedData>();

        public void Fill(Dictionary<object, Dictionary<Type, object>> container)
        {
            Clear();
            foreach (var context in container)
            {
                var objectSerializedData = new ObjectSerializedData();
                objectSerializedData.context = context.Key.GetType().Name;
                objectSerializedData.references = new List<string>();
                foreach (var reference in context.Value)
                {
                    objectSerializedData.references.Add(reference.Key.Name);
                    objectsCount++;
                }
                objectsData.Add(objectSerializedData);
            }
        }
        
        public void Clear()
        {
            objectsCount = 0;
            objectsData.Clear();
        }
    }
}