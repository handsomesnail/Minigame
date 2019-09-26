using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Item;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using ZCore;

[CustomEditor (typeof (Item))]
public class CollectItemEditor : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector ();
        Item item = (Item)target;
        if (!EditorUtility.IsPersistent (target)) {
            if (GUILayout.Button ("保存")) {
                if (EditorUtility.DisplayDialog ("防手抖", "注意要保存的收集品名为\"" + item.gameObject.name + "\"(和GameObject名相同)，如果该命名的地图存在则会覆盖原有版本", "确定保存", "取消")) {
                    SaveItem ();
                }
            }
        }
    }

    private void SaveItem () {
        Item item = (Item)target;
        string itemName = item.gameObject.name;
        string path = "Assets/Prefabs/Maps/" + itemName + ".prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject> (path);
        if (prefab == null) {
            PrefabUtility.CreatePrefab (path, item.gameObject);
        } else {
            PrefabUtility.ReplacePrefab (item.gameObject, prefab, ReplacePrefabOptions.ReplaceNameBased);
        }
        string sceneName = "EditCollectItem_" + item.gameObject.name;
        string scenePath = "Assets/Scenes/" + sceneName + ".unity";
        EditorSceneManager.SaveScene (item.gameObject.scene, scenePath);
        ToolsHelperEditor.SetStartupScene (itemName);
    }

}