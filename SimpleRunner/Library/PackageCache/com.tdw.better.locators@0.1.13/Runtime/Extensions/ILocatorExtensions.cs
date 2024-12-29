using System;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using Better.Commons.Runtime.Utility;
using Better.Locators.Runtime;

public static class ILocatorExtensions
{
    public static bool TryAdd<TElement>(this ILocator<Type, TElement> self, TElement element)
    {
        if (self == null)
        {
            DebugUtility.LogException<ArgumentNullException>(nameof(self));
            return false;
        }

        if (element == null)
        {
            DebugUtility.LogException<ArgumentNullException>(nameof(element));
            return false;
        }

        var key = element.GetType();
        return self.TryAdd(key, element);
    }

    public static bool TryAdd<TElement>(this ILocator<SerializedType, TElement> self, TElement element)
    {
        if (self == null)
        {
            DebugUtility.LogException<ArgumentNullException>(nameof(self));
            return false;
        }

        if (element == null)
        {
            DebugUtility.LogException<ArgumentNullException>(nameof(element));
            return false;
        }

        var key = element.GetType();
        return self.TryAdd(key, element);
    }

    public static void Add<TKey, TElement>(this ILocator<TKey, TElement> self, TKey key, TElement element)
    {
        if (self == null)
        {
            DebugUtility.LogException<ArgumentNullException>(nameof(self));
            return;
        }

        if (key == null)
        {
            DebugUtility.LogException<ArgumentNullException>(nameof(key));
            return;
        }

        if (element == null)
        {
            DebugUtility.LogException<ArgumentNullException>(nameof(element));
            return;
        }

        if (!self.TryAdd(key, element))
        {
            var message = $"Invalid adding {nameof(element)} by {nameof(key)}({key})";
            DebugUtility.LogException<ArgumentNullException>(message);
        }
    }

    public static void Add<TElement>(this ILocator<Type, TElement> self, TElement element)
    {
        if (element == null)
        {
            DebugUtility.LogException<ArgumentNullException>(nameof(element));
            return;
        }

        var key = element.GetType();
        self.Add(key, element);
    }

    public static void Add<TElement>(this ILocator<SerializedType, TElement> self, TElement element)
    {
        if (element == null)
        {
            DebugUtility.LogException<ArgumentNullException>(nameof(element));
            return;
        }

        var key = element.GetType();
        self.Add(key, element);
    }

    public static bool ContainsKey<TElement, TDerived>(this ILocator<Type, TElement> self)
        where TDerived : TElement
    {
        var key = typeof(TDerived);
        return self.ContainsKey(key);
    }

    public static bool ContainsKey<TElement, TDerived>(this ILocator<SerializedType, TElement> self)
        where TDerived : TElement
    {
        var key = typeof(TDerived);
        return self.ContainsKey(key);
    }

    public static bool TryGet<TKey, TElement, TDerived>(this ILocator<TKey, TElement> self, TKey key, out TDerived element)
        where TDerived : TElement
    {
        if (self == null)
        {
            DebugUtility.LogException<ArgumentNullException>(nameof(self));
            element = default;
            return false;
        }

        if (self.TryGet(key, out var located)
            && located is TDerived casted)
        {
            element = casted;
            return true;
        }

        element = default;
        return false;
    }

    public static bool TryGet<TElement, TDerived>(this ILocator<Type, TElement> self, out TDerived element)
        where TDerived : TElement
    {
        var key = typeof(TDerived);
        return self.TryGet(key, out element);
    }

    public static bool TryGet<TElement, TDerived>(this ILocator<SerializedType, TElement> self, out TDerived element)
        where TDerived : TElement
    {
        var key = typeof(TDerived);
        return self.TryGet(key, out element);
    }

    public static TElement Get<TKey, TElement>(this ILocator<TKey, TElement> self, TKey key)
    {
        if (self == null)
        {
            DebugUtility.LogException<ArgumentNullException>(nameof(self));
            return default;
        }

        if (!self.TryGet(key, out var element))
        {
            var message = $"Invalid getting {nameof(element)} by {nameof(key)}({key})";
            DebugUtility.LogException<InvalidOperationException>(message);
            return default;
        }

        return element;
    }

    public static TDerived Get<TElement, TDerived>(this ILocator<Type, TElement> self)
        where TDerived : TElement
    {
        if (self == null)
        {
            DebugUtility.LogException<ArgumentNullException>(nameof(self));
            return default;
        }

        var type = typeof(TDerived);
        var element = (TDerived)self.Get(type);
        if (element == null)
        {
            var message = $"Invalid getting {nameof(element)} by {nameof(type)}({type})";
            DebugUtility.LogException<InvalidOperationException>(message);
        }

        return element;
    }

    public static TDerived Get<TElement, TDerived>(this ILocator<SerializedType, TElement> self)
        where TDerived : TElement
    {
        if (self == null)
        {
            DebugUtility.LogException<ArgumentNullException>(nameof(self));
            return default;
        }

        var type = typeof(TDerived);
        var element = (TDerived)self.Get(type);
        if (element == null)
        {
            var message = $"Invalid getting {nameof(element)} by {nameof(type)}({type})";
            DebugUtility.LogException<InvalidOperationException>(message);
        }

        return element;
    }
}