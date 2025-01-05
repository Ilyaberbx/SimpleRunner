using System;
using Factura.UI.Services;
using UnityEngine;

namespace Factura.UI.Helpers
{
    public static class PopupsLogHelper
    {
        private const string CannotFindControllerFormat = "Can not find controller for {0}";
        private const string CanNotFindViewFormat = "Can not find view for {0}";
        private const string MappingMismatchedFormat = "Wrong mapping controller {0} with {1}. " +
                                                       "Mapped Controller for {1} => {2}";

        public static void LogCannotFindView(PopupType type)
        {
            var message = string.Format(CanNotFindViewFormat, type.ToString());
            Debug.LogError(message);
        }

        public static void LogMappingMismatched<TRequestedController>(Type actualControllerType, PopupType type)
        {
            var requestedControllerType = typeof(TRequestedController);

            var message = string.Format(MappingMismatchedFormat,
                requestedControllerType,
                type.ToString(),
                actualControllerType);

            Debug.LogError(message);
        }

        public static void LogCannotFindController(PopupType type)
        {
            var message = string.Format(CannotFindControllerFormat, type.ToString());
            Debug.LogError(message);
        }
    }
}