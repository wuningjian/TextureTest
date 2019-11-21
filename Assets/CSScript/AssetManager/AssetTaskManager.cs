using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace Assets.CSScript
{
    public class AssetTaskManager:Singleton<AssetTaskManager>
    {
        private LinkedListNode<AssetTask> mCurTaskNode = null;
        private LinkedList<AssetTask> mTaskList = new LinkedList<AssetTask>();
        private Stack<LinkedListNode<AssetTask>> mTaskNodeStack = new Stack<LinkedListNode<AssetTask>>();
        private Stopwatch mWatch = new Stopwatch();
        public void Update()
        {
            mWatch.Reset();
            mWatch.Start();
            mCurTaskNode = mTaskList.First;
            LinkedListNode<AssetTask> tempNode = null;

            while ((mCurTaskNode != null) && (!IsOverTime()))
            {
                if (mCurTaskNode.Value.Update())
                {
                    mCurTaskNode.Value.End();
                    mCurTaskNode.Value.Reset();
                    tempNode = mCurTaskNode;
                    mCurTaskNode = mCurTaskNode.Next;
                    mTaskList.Remove(tempNode);
                    tempNode.Value = null;
                    mTaskNodeStack.Push(tempNode);
                }
                else
                {

                }
            }
            mWatch.Stop();
        }

        // 加IsOverTime是为了防止某些资源加载太久，while循环卡住Update
        public bool IsOverTime()
        {
            return mWatch.ElapsedMilliseconds > 2;
        }

        public void AddLoadAssetTask(string bundleName, string assetName)
        {
            LoadAssetTask loadAssetTask = new LoadAssetTask();
            loadAssetTask.SetBundleNameAndAssetName(bundleName, assetName);
            if (mTaskNodeStack.Count > 0)
            {
                LinkedListNode<AssetTask> node = mTaskNodeStack.Pop();
                node.Value = loadAssetTask;
                mTaskList.AddLast(node);
            }
            else
            {
                mTaskList.AddLast(loadAssetTask);
            }
            loadAssetTask.Start();
        }

        public int GetTaskNum()
        {
            return mTaskList.Count;
        }
    }
}
