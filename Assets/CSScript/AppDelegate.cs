using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using Assets.CSScript;

public class AppDelegate : MonoBehaviour {

    //public GameObject m_PigTreasureView = null;
    public GameObject m_UIRoot = null;
    public UIPrefabDesc m_PigTreasureViewDesc = null;
    private bool mIsOpenView = false;
    private bool mLoadFinish = false;
    private string m_BundleName = "ui_desc.ab";
    private string m_AssetName = "ui_pig_treasure_view_desc";
    // Use this for initialization
    //void Start () {
    //       //Debug.Log("保留内存: " + Profiler.GetTotalReservedMemoryLong()/1024/1024 + "MBytes");
    //       //      Debug.Log("- 已分配: " + Profiler.GetTotalAllocatedMemoryLong() / 1024 / 1024 + "MBytes");
    //       //      Debug.Log("- 剩余: " + Profiler.GetTotalUnusedReservedMemoryLong() / 1024 / 1024 + "MBytes");

    //       //通过object对象名 face 得到网格渲染器对象
    //       MeshFilter meshFilter = (MeshFilter)GameObject.Find("face").GetComponent(typeof(MeshFilter));
    //       MeshHelper.Instence.CreateATriangleMesh(meshFilter);

    //       UIManager.Instence.Start();

    //       Transform pig_view_tran = m_PigTreasureViewDesc.CreateUIObj();
    //       UIManager.Instence.AddTranToUIRoot(pig_view_tran);
    //   }

    private void Start()
    {
        //print("Starting " + Time.time);
        //yield return StartCoroutine(WaitAndPrint(2.0F)); // yield return 就是等待当前协程完成，再继续执行下面代码。相当于阻塞。
        //print("Done " + Time.time);

        //StartCoroutine("DoSomething", 1.0);
        //print("Done " + Time.time);
        //yield return new WaitForSeconds(2);
        //StopCoroutine("DoSomething");

        UIManager.Instence.Start();
        StartCoroutine(PreloadAsset());

    }

    IEnumerator PreloadAsset()
    {
        AssetTaskManager.Instence.AddLoadAssetTask(m_BundleName, m_AssetName);
        yield return WaitForLoadFinish();

        mLoadFinish = true;
    }

    private IEnumerator WaitForLoadFinish()
    {
        while (AssetTaskManager.Instence.GetTaskNum() > 0)
        {
            print("task num:" + AssetTaskManager.Instence.GetTaskNum());
            yield return null;
        }
        yield break;
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        print("WaitAndPrint " + Time.time);
    }

    IEnumerator DoSomething(float param)
    {
        int i = 1;
        while (i < 100)
        {
            print("DoSomething Loop " + i);
            i++;
            yield return null; 
            //if (i<100)
            //{
            //    yield return null;  // yield return null 相当于没有阻塞，一直进行下去
            //}
            //else
            //{
            //    yield break;  // yield break 返回当前协程
            //}
            
        }
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update () {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    if (!is_open_view && m_PigTreasureView != null)
        //    {
        //        Debug.Log("- 已分配: " + Profiler.GetTotalAllocatedMemoryLong() / 1024 / 1024 + "MBytes");
        //        Debug.Log("- 剩余: " + Profiler.GetTotalUnusedReservedMemoryLong() / 1024 / 1024 + "MBytes");
        //        //is_open_view = true;
        //        //if(m_UIRoot != null)
        //        //{
        //        //    pig_view.transform.SetParent(m_UIRoot.transform);
        //        //}

        //        Debug.Log("- 已分配: " + Profiler.GetTotalAllocatedMemoryLong() / 1024 / 1024 + "MBytes");
        //        Debug.Log("- 剩余: " + Profiler.GetTotalUnusedReservedMemoryLong() / 1024 / 1024 + "MBytes");
        //    }
        //}
        AssetTaskManager.Instence.Update();
        if (mLoadFinish && !mIsOpenView)
        {
            m_PigTreasureViewDesc = AssetManager.Instence.GetAssetObjWithType<UIPrefabDesc>(m_BundleName, m_AssetName);
            Transform pig_view = m_PigTreasureViewDesc.CreateUIObj();
            UIManager.Instence.AddTranToUIRoot(pig_view);
            mIsOpenView = true;
        }
    }


}
