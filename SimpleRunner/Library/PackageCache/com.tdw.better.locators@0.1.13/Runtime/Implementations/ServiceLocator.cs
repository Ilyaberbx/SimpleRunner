#if BETTER_SERVICES
using System;
using Better.Commons.Runtime.Utility;
using Better.Services.Runtime.Interfaces;
using UnityEngine;

namespace Better.Locators.Runtime
{
    public static class ServiceLocator
    {
        private static readonly Locator<IService> _source;

        static ServiceLocator()
        {
            _source = new Locator<IService>();
        }

        public static void Register<TService>(TService service)
            where TService : IService
        {
            if (service == null)
            {
                DebugUtility.LogException<ArgumentNullException>(nameof(service));
                return;
            }

            if (service is { Initialized: false })
            {
                var type = service.GetType();
                var message = $"{nameof(service)} of {nameof(type)}{type} not initialized";
                Debug.LogWarning(message);
            }

            if (HasRegistered<TService>())
            {
                var type = typeof(TService);
                var message = $"{nameof(service)} of {nameof(type)}({type}) already registered";
                DebugUtility.LogException<InvalidOperationException>(message);

                return;
            }

            if (!_source.TryAdd(service))
            {
                DebugUtility.LogException<InvalidOperationException>();
            }
        }

        public static bool HasRegistered<TService>()
            where TService : IService
        {
            return _source.ContainsKey<IService, TService>();
        }

        public static bool TryGet<TService>(out TService service)
            where TService : IService
        {
            return _source.TryGet(out service);
        }

        public static TService Get<TService>()
            where TService : IService
        {
            if (!TryGet(out TService service))
            {
                DebugUtility.LogException<InvalidOperationException>();
            }

            return service;
        }
        
        public static void Unregister<TService>(TService service)
            where TService : IService
        {
            if (service == null)
            {
                DebugUtility.LogException<ArgumentNullException>(nameof(service));
                return;
            }

            if (!HasRegistered<TService>())
            {
                var type = typeof(TService);
                var message = $"{nameof(service)} of {nameof(type)}({type}) not registered";
                DebugUtility.LogException<InvalidOperationException>(message);

                return;
            }

            if (!_source.Remove(service))
            {
                DebugUtility.LogException<InvalidOperationException>();
            }
        }
    }
}

#endif