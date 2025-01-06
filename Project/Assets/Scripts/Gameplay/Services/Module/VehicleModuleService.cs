using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using DG.Tweening;
using Factura.Gameplay.BulletsPack;
using Factura.Gameplay.Car;
using Factura.Global.Services.StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factura.Gameplay.Services.Module
{
    [Serializable]
    public sealed class VehicleModuleService : PocoService
    {
        private const string FactoryAlreadyRegisteredFormat = "Factory for {0} already registered";
        private const string CanNotFindFactoryFormat = "Can not find factory for {0}";

        private readonly IDictionary<Type, IVehicleModuleFactory> _typeFactoryMap = new Dictionary<Type, IVehicleModuleFactory>();

        private readonly IDictionary<Type, BaseModuleConfiguration> _configurationsTypeMap =
            new Dictionary<Type, BaseModuleConfiguration>();

        private IGameplayStaticDataProvider _gameplayStaticDataProvider;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _gameplayStaticDataProvider = ServiceLocator.Get<GameplayStaticDataService>();

            var carConfiguration = _gameplayStaticDataProvider.GetModuleConfiguration(VehicleModuleType.Car);
            var turretConfiguration = _gameplayStaticDataProvider.GetModuleConfiguration(VehicleModuleType.Turret);
            var bulletsPackConfiguration = _gameplayStaticDataProvider.GetModuleConfiguration(VehicleModuleType.BulletsPack);

            _configurationsTypeMap.Add(typeof(CarConfiguration), carConfiguration);
            _configurationsTypeMap.Add(typeof(TurretConfiguration), turretConfiguration);
            _configurationsTypeMap.Add(typeof(BulletsPackConfiguration), bulletsPackConfiguration);

            return Task.CompletedTask;
        }

        public TConfiguration GetConfiguration<TConfiguration>() where TConfiguration : BaseModuleConfiguration
        {
            var configurationType = typeof(TConfiguration);

            if (_configurationsTypeMap.TryGetValue(configurationType, out var derivedConfiguration))
            {
                if (derivedConfiguration is TConfiguration concreteConfiguration)
                {
                    return concreteConfiguration;
                }
            }

            return null;
        }

        public void RegisterFactory<TModule>(IVehicleModuleFactory factory) where TModule : VehicleModuleBehaviour
        {
            var moduleType = typeof(TModule);
            var addedSuccessfully = _typeFactoryMap.TryAdd(moduleType, factory);

            if (addedSuccessfully)
            {
                return;
            }

            var message = string.Format(FactoryAlreadyRegisteredFormat, moduleType.Name);
            Debug.LogWarning(message);
        }

        public void UnregisterAllFactories()
        {
            _typeFactoryMap.Clear();
        }

        public TModule Create<TModule>(Vector3 at = default) where TModule : VehicleModuleBehaviour
        {
            var moduleType = typeof(TModule);
            var hasFactory = _typeFactoryMap.TryGetValue(moduleType, out var factory);

            if (hasFactory)
            {
                var derivedModule = factory.Create(at);

                if (derivedModule is TModule concreteModule)
                {
                    return concreteModule;
                }
            }

            var message = string.Format(CanNotFindFactoryFormat, moduleType);
            Debug.LogError(message);
            return null;
        }

        public void Release(VehicleModuleBehaviour module)
        {
            DOTween.Kill(module);
            Object.Destroy(module.gameObject);
        }
    }
}