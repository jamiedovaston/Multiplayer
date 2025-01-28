using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Model.DataAsset
{
    public abstract class DataAsset<T> : ScriptableObject where T : ScriptableObject
    {
        private static List<T> _collection = null;
        public static List<T> collection
        {
            get
            {
                if (_collection == null)
                {
                    _collection = Resources.LoadAll<T>(GetResourcePath()).ToList();
                }
                return _collection;
            }
        }

        public static T Get(string _id)
        {
            T obj = collection.Find(t => t.name == _id);
            if (obj != null) return obj;
            Debug.LogError($"Could not find data asset with ID: {_id}!");
            return null;
        }

        protected static string GetResourcePath() => string.Empty;
    }
}
