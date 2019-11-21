using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.CSScript
{
    public class AssetManager: Singleton<AssetManager>
    {
        private Dictionary<string, AsyncOperation> mUIAssetRequestMap = new Dictionary<string, AsyncOperation>();
        private Dictionary<string, Object> mUIAssetObjMap = new Dictionary<string, Object>();

        private bool mIsResourcesMode = true; // true Resource模式load，false assetbundle模式load

        // bundleName:ui/ui_common.ab assetName:ui_pig_treasure_view_desc.asset
        public AsyncOperation LoadAssetAsync(string bundleName, string assetName)
        {
            AsyncOperation oper = null;
            string path = TransferPathToResourceMode(bundleName, assetName);
            if (mUIAssetRequestMap.TryGetValue(path, out oper))
            {
                return oper;
            }
            else
            {
                if (mIsResourcesMode) {
                    // 编辑器开发环境时，在这里加载
                    oper = Resources.LoadAsync(path);
                }
                else
                {
                    // 打成asset bundle就在这里加载
                }

                Debug.Log("add oper");
                mUIAssetRequestMap.Add(path, oper);
                return oper;
            }
        }

        public void OnLoadAsset(string bundleName, string assetName)
        {
            string path = TransferPathToResourceMode( bundleName, assetName);
            Debug.Log("on loadasset success:" + path);
            AsyncOperation oper = null;
            mUIAssetRequestMap.TryGetValue(path, out oper);
            if (oper!=null)
            {
                Debug.Log("get oper");
                ResourceRequest finishOper = oper as ResourceRequest;
                mUIAssetObjMap.Add(path, finishOper.asset);
            }
        }

        public T GetAssetObjWithType<T>(string bundleName, string assetName) where T : class
        { 
            string path = TransferPathToResourceMode(bundleName, assetName);
            Object obj = null;
            mUIAssetObjMap.TryGetValue(path, out obj);
            return obj as T;
        }

        public string TransferPathToResourceMode(string bundleName, string assetName)
        {
            return bundleName.Remove(bundleName.LastIndexOf('.')) + "/" + assetName;
        }
    }
}
