#if BETTER_LOCATORS && BETTER_SERVICES
using System;
using Better.Attributes.Runtime.Select;
using Better.Services.Runtime;
using UnityEngine;

namespace Better.Contexts.Runtime.Installers
{
    [Serializable]
    public class PocoServicesInstaller : ServicesInstaller<PocoService>
    {
        [Select]
        [SerializeReference] private PocoService[] _services;

        protected override PocoService[] Services => _services;
    }
}
#endif