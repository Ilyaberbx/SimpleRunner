using System;
using Factura.UI.Services;
using UnityEngine;

namespace Factura.UI.Helpers
{
    public static class PopupsDebugHelper
    {
        private const string CanNotFindViewFormat = "Can not find view for {0}";

        private const string MappingMismatchedFormat = "Wrong mapping controller {0} with {1}. " +
                                                       "Mapped Controller for {1} => {2}";

        private const string CannotFindControllerFormat = "Can not find controller for {0}";

        public static void PrintCannotFindView(PopupType type)
        {
            var message = string.Format(CanNotFindViewFormat, type.ToString());
            Debug.LogError(message);
        }

        public static void PrintMappingMismatched<TRequestedController>(Type actualControllerType, PopupType type)
        {
            var requestedControllerType = typeof(TRequestedController);

            var message = string.Format(MappingMismatchedFormat,
                requestedControllerType,
                type.ToString(),
                actualControllerType);

            Debug.LogError(message);
        }

        public static void PrintCannotFindController(PopupType type)
        {
            var message = string.Format(CannotFindControllerFormat, type.ToString());
            Debug.LogError(message);
        }
    }
}