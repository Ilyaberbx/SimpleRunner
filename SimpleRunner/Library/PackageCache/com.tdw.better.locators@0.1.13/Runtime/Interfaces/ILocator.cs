using System;

namespace Better.Locators.Runtime
{
    public interface ILocator<TKey, TElement>
    {
        public event Action Changed;
        public event Locator<TKey, TElement>.OnElementChanged ElementAdded;
        public event Locator<TKey, TElement>.OnElementChanged ElementRemoved;
        
        public int Count { get; }
        
        public bool TryAdd(TKey key, TElement element);
        public bool ContainsKey(TKey key);
        public bool ContainsElement(TElement element);
        public TKey[] GetKeys();
        public TElement[] GetElements();
        public bool TryGet(TKey key, out TElement element);
        public bool Remove(TKey key);
        public bool Remove(TElement element);
        public void Clear();
    }
}