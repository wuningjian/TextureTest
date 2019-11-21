using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.CSScript
{
    public class LoadAssetTask:AssetTask
    {
        private AsyncOperation mLoadAsyncOperation = null;
        private string mBundleName = null;
        private string mAssetName = null;
        public void SetBundleNameAndAssetName(string bundleName, string assetName)
        {
            mBundleName = bundleName;
            mAssetName = assetName;
        }
        public override TaskType TaskType
        {
            get
            {
                return TaskType.LoadAsset;
            }
        }

        public override bool Start()
        {
            mLoadAsyncOperation = AssetManager.Instence.LoadAssetAsync(mBundleName, mAssetName);
            return false;
        }

        public override void End()
        {
            AssetManager.Instence.OnLoadAsset(mBundleName, mAssetName);
        }

        public override bool Update()
        {
            return mLoadAsyncOperation == null || mLoadAsyncOperation.isDone;
        }

        public override void Reset()
        {
            mLoadAsyncOperation = null;
            mBundleName = null;
            mAssetName = null;
        }

    }
}
