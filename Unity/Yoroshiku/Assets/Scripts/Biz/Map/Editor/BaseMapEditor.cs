using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Map;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using ZCore;

[CustomEditor(typeof(BaseMap))]
public class BaseMapEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        BaseMap baseMap = (BaseMap) target;
        if (!EditorUtility.IsPersistent(target)) {
            if (GUILayout.Button("保存")) {
                if (EditorUtility.DisplayDialog("防手抖", "注意要保存的地图名为\"" + baseMap.gameObject.name + "\"(和GameObject名相同)，如果该命名的地图存在则会覆盖原有版本", "确定保存", "取消")) {
                    SaveMap();
                }
            }
        }
    }

    private void SaveMap() {
        BaseMap baseMap = (BaseMap) target;
        string mapName = baseMap.gameObject.name;
        string path = "Assets/Prefabs/Maps/" + mapName + ".prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab == null) {
            PrefabUtility.CreatePrefab(path, baseMap.gameObject);
        }
        else {
            PrefabUtility.ReplacePrefab(baseMap.gameObject, prefab, ReplacePrefabOptions.ReplaceNameBased);
        }
        string sceneName = "Edit" + baseMap.gameObject.name;
        string scenePath = "Assets/Scenes/" + sceneName + ".unity";
        EditorSceneManager.SaveScene(baseMap.gameObject.scene, scenePath);
        ToolsHelperEditor.SetStartupScene(mapName);
    }

}