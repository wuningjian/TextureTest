using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UIPrefabDesc : ScriptableObject
{
    [SerializeField]
    private GameObject mUIPrefab = null;

    public void SetUIPrefab(GameObject obj)
    {
        mUIPrefab = obj;
    }

    public Transform CreateUIObj()
    {
        if(mUIPrefab == null)
        {
            return null;
        }
        GameObject obj = GameObject.Instantiate<GameObject>(mUIPrefab);
        return obj.transform;
    }
}

    
