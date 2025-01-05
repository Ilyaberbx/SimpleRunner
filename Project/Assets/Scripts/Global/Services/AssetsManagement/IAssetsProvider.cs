using System.Threading.Tasks;
using UnityEngine;

namespace Factura.Global.Services.AssetsManagement
{
    public interface IAssetsProvider
    {
        Task<TAsset[]> LoadAll<TAsset>(string address) where TAsset : Object;
        Task<TAsset> Load<TAsset>(string address) where TAsset : Object;
    }
}