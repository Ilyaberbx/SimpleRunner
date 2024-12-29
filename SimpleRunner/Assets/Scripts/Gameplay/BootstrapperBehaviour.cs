using Better.Locators.Runtime;
using Gameplay.Services;
using Gameplay.Vehicle.Modules;
using UnityEngine;

namespace Gameplay
{
    public class BootstrapperBehaviour : MonoBehaviour
    {
        private void Start()
        {
            var moduleService = ServiceLocator.Get<ModuleService>();

            moduleService.Create<TurretBehaviour>();
        }
    }
}