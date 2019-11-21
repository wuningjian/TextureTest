using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UIPrefabProcessor {
    //Assets/Resources/ui/ui_pig_treasure/ui_pig_treasure_view.prefab
    //private static string UIResPath = "Assets/Resources/ui/";
    private static string UIDescPath = "Assets/Resources/ui_desc/";
    private static string DescPostName = "_desc.asset";

    [MenuItem("Assets/导出UI信息")]
    static void CreateUIPrefabDesc()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        Debug.Log(path);

        UIPrefabDesc desc = ScriptableObject.CreateInstance<UIPrefabDesc>();
        desc.SetUIPrefab((GameObject)Selection.activeObject);

        string savePath = (UIDescPath + path.Remove(0, path.LastIndexOf('/') + 1)).Replace(".prefab", DescPostName);
        Debug.LogFormat("save path {0}" , savePath);
        AssetDatabase.CreateAsset(desc, savePath);
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/导出UI信息", true)]
    public static bool ValidCreateUIPrefabDesc()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path.EndsWith(".prefab"))
        {
            return true;
        }
        return false;
    }

}
