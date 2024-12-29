using System;
using UnityEngine;

namespace Better.Locators.Runtime
{
    public abstract class MonoLocator<TKey, TElement> : MonoBehaviour, ILocator<TKey, TElement>
    {
        public event Action Changed
        {
            add => Source.Changed += value;
            remove => Source.Changed -= value;
        }

        public event Locator<TKey, TElement>.OnElementChanged ElementAdded
        {
            add => Source.ElementAdded += value;
            remove => Source.ElementAdded -= value;
        }

        public event Locator<TKey, TElement>.OnElementChanged ElementRemoved
        {
            add => Source.ElementRemoved += value;
            remove => Source.ElementRemoved -= value;
        }

        private ILocator<TKey, TElement> _source;

        public int Count => Source.Count;

        protected ILocator<TKey, TElement> Source
        {
            get
            {
                if (_source == null)
                {
                    _source = new Locator<TKey, TElement>();
                }

                return _source;
            }
        }

        public bool TryAdd(TKey key, TElement element)
        {
            return Source.TryAdd(key, element);
        }

        public bool ContainsKey(TKey key)
        {
            return Source.ContainsKey(key);
        }

        public bool ContainsElement(TElement element)
        {
            return Source.ContainsElement(element);
        }

        public TKey[] GetKeys()
        {
            return Source.GetKeys();
        }

        public TElement[] GetElements()
        {
            return Source.GetElements();
        }

        public bool TryGet(TKey key, out TElement element)
        {
            return Source.TryGet(key, out element);
        }

        public bool Remove(TKey key)
        {
            return Source.Remove(key);
        }

        public bool Remove(TElement element)
        {
            return Source.Remove(element);
        }

        public void Clear()
        {
            Source.Clear();
        }
    }

    public abstract class MonoLocator<TElement> : MonoLocator<Type, TElement>
    {
    }

    public class MonoLocator : MonoLocator<Type, MonoBehaviour>
    {
    }
}