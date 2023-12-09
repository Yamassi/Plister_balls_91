using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tretimi
{
    public static class Assets
    {
        public static async Task<T> GetAsset<T>(string name)
        {
            Task<T> asyncOperationHandler = Addressables.LoadAssetAsync<T>(name).Task;
            T result = await asyncOperationHandler;
            return result;
        }
    }
}
