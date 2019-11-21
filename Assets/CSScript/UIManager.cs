using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace Assets.CSScript
{
    class UIManager:Singleton<UIManager>
    {
        public static Vector2 ResolutionSize = new Vector2(1280.0f, 720.0f);

        private Vector2 mUISize;
        private float mScaleFactor = 1.0f;
        private Camera mCamera = null;
        private GameObject mUIRoot = null;
        private Transform mUIRootTrans = null;
        public static UIManager GetInstance()
        {
            return Instence;
        }

        public void Start()
        {
            GameObject camObj = new GameObject("_UICamera");
            mCamera = CreateUICamera(camObj);
            Object.DontDestroyOnLoad(camObj);

            mUIRoot = new GameObject("_UIRoot");
            mUIRoot.layer = LayerMask.NameToLayer("UI");

            // 设置canvas
            Canvas canvas = mUIRoot.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera; // unity书
            canvas.worldCamera = mCamera;
            canvas.planeDistance = 1000.0f;

            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            float scaleX = screenWidth / ResolutionSize.x;
            float scaleY = screenHeight / ResolutionSize.y;

            mUIRootTrans = mUIRoot.transform;
            mUIRootTrans.position = Vector3.zero;
            mUIRootTrans.SetParent(camObj.transform, false);

            CanvasScaler scaler = mUIRoot.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = ResolutionSize;

            GameObject ev = new GameObject("_EventSystem");
            ev.AddComponent<EventSystem>();
            ev.AddComponent<StandaloneInputModule>();
            GameObject.DontDestroyOnLoad(ev);

            // 计算并记录和处理屏幕宽高比
            float aspectRatio = screenWidth / screenHeight;
            float designedAspectRatio = ResolutionSize.x / ResolutionSize.y;
            if (aspectRatio < designedAspectRatio)
                mScaleFactor = 1.0f / (((ResolutionSize.x / screenWidth) * screenHeight) / ResolutionSize.y);
            else
                mScaleFactor = 1.0f;

            if (scaleX > scaleY)
            {
                mUISize.y = ResolutionSize.y;
                mUISize.x = screenWidth / screenHeight * ResolutionSize.y;
            }
            else
            {
                mUISize.x = ResolutionSize.x;
                mUISize.y = screenHeight / screenWidth * ResolutionSize.x;
            }
        }

        public void GetFullScreenUISize(out float width, out float height)
        {
            width = mUISize.x;
            height = mUISize.y;
        }

        public void GetUIScaleFactor(out float factor)
        {
            factor = mScaleFactor;
        }

        private Camera CreateUICamera(GameObject obj)
        {
            Camera cam = obj.AddComponent<Camera>();

            // 每个相机在渲染时会存储颜色和深度信息。屏幕的未绘制部分是空的，默认情况下会显示天空盒。
            // 当你使用多个相机时，每一个都将自己的颜色和深度信息存储在缓冲区中，还将积累大量的每个相机的渲染数据。
            // 当场景中的任何特定相机进行渲染时，你可以设定清除标记以清除缓冲区信息的不同集合
            cam.clearFlags = CameraClearFlags.Color;

            cam.cullingMask = 1 << LayerMask.NameToLayer("UI"); // 在摄像机视椎体，只有UI标签的obj被绘制

            cam.nearClipPlane = 0.1f;
            cam.farClipPlane = 1010.0f;
            cam.orthographic = true;
            cam.orthographicSize = ResolutionSize.y / 2.0f / 100.0f; // 用一半的垂直方向做size比例，100pixel为1单位，x会自动根据纵横比生成
            cam.depth = 100; // 多摄像机时生效，depth越大，越在上层
            cam.allowHDR = false; // 动态光照渲染(增强细节) https://blog.csdn.net/zkl99999/article/details/45070139
            cam.allowMSAA = false; // 多重采样抗锯(使物体边缘更平滑)
            cam.backgroundColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            cam.transform.position = new Vector3(0.0f, 0.0f, -10000.0f);
            cam.useOcclusionCulling = false;
            cam.backgroundColor = Color.white;

            return cam;
        }

        public void AddTranToUIRoot(Transform tran)
        {
            tran.SetParent(mUIRootTrans, false);
        }


        //public Transform CreateLayout(string path)
        //{
        //    //ScriptableObject desc = GameObject.

        //}
    }
}
