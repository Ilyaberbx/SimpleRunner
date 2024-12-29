using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Utility;
using UnityEngine;

namespace Better.Locators.Runtime
{
    [Serializable]
    public class Locator<TKey, TElement> : ILocator<TKey, TElement>
    {
        public event Action Changed;
        public event OnElementChanged ElementAdded;
        public event OnElementChanged ElementRemoved;

        [SerializeField] private SerializedDictionary<TKey, TElement> _sourceMap;

        public int Count => _sourceMap.Count;

        public Locator(IEqualityComparer<TKey> comparer)
        {
            _sourceMap = new(comparer);
        }

        public Locator() : this(EqualityComparer<TKey>.Default)
        {
        }

        public delegate void OnElementChanged(TKey key, TElement element);

        public bool TryAdd(TKey key, TElement element)
        {
            if (element == null)
            {
                return false;
            }

            if (ContainsElement(element))
            {
                return false;
            }

            _sourceMap.Add(key, element);
            OnAdded(key, element);
            return true;
        }

        protected virtual void OnAdded(TKey key, TElement element)
        {
            ElementAdded?.Invoke(key, element);
            OnChanged();
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
            {
                DebugUtility.LogException<InvalidOperationException>(nameof(key));
                return false;
            }

            return _sourceMap.ContainsKey(key);
        }

        public bool ContainsElement(TElement element)
        {
            if (element == null)
            {
                DebugUtility.LogException<InvalidOperationException>(nameof(element));
                return false;
            }

            return _sourceMap.ContainsValue(element);
        }

        public TKey[] GetKeys()
        {
            return _sourceMap.Keys.ToArray();
        }

        public TElement[] GetElements()
        {
            return _sourceMap.Values.ToArray();
        }

        public bool TryGet(TKey key, out TElement element)
        {
            if (key == null)
            {
                DebugUtility.LogException<ArgumentNullException>(nameof(key));
                element = default;
                return false;
            }

            return _sourceMap.TryGetValue(key, out element);
        }

        public bool Remove(TKey key)
        {
            if (key == null)
            {
                DebugUtility.LogException<ArgumentNullException>(nameof(key));
                return false;
            }

            var removed = _sourceMap.Remove(key, out var element);
            if (removed)
            {
                OnRemoved(key, element);
            }

            return removed;
        }

        public bool Remove(TElement element)
        {
            if (element == null)
            {
                DebugUtility.LogException<ArgumentNullException>(nameof(element));
                return false;
            }

            if (_sourceMap.Remove(element, out var key))
            {
                OnRemoved(key, element);
                return true;
            }

            return false;
        }

        protected virtual void OnRemoved(TKey key, TElement element)
        {
            ElementRemoved?.Invoke(key, element);
            OnChanged();
        }

        public void Clear()
        {
            _sourceMap.Clear();
            OnChanged();
        }

        protected virtual void OnChanged()
        {
            Changed?.Invoke();
        }
    }

    [Serializable]
    public class Locator<TElement> : Locator<SerializedType, TElement>//, ISerializationCallbackReceiver
    {
        // [SerializeField] private TElement[] _elements;
        //
        // void ISerializationCallbackReceiver.OnBeforeSerialize()
        // {
        //     _elements = GetElements();
        // }
        //
        // void ISerializationCallbackReceiver.OnAfterDeserialize()
        // {
        //     if (_elements == null)
        //     {
        //         DebugUtility.LogException<SerializationException>();
        //         return;
        //     }
        //
        //     Clear();
        //     foreach (var element in _elements)
        //     {
        //         this.TryAdd(element);
        //     }
        // }
    }
}