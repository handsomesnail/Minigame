using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Biz.Map;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CollectItemEditorWindow : EditorWindow {
    public const string templetePath = "Assets/Prefabs/MapObjects/CollectItem.prefab";
    public const string editMapTempletePath = "Assets/Scenes/CollectItemTpl.unity";
    public const string mainScenePath = "Assets/Scenes/Index.unity";
    public const string mapListPath = "Assets/Resources/Configs/MapList.asset";

    private List<string> mapsNameArr;
    private int selectIndex;
    private string inputMapName;
    private MapList mapList;
    private GUIStyle style;

    private void OnEnable () {
        //selectIndex = 0;
        //SetCurrentSceneIndex ();
        //mapList = AssetDatabase.LoadAssetAtPath<MapList> ("Assets/Resources/Configs/MapList.asset");
    }

    public void OnGUI () {
        EditorGUILayout.Space ();
        //UpdateAllEditableMapsName ();
        style = new GUIStyle (GUI.skin.label);
        //EditorGUILayout.LabelField ("开始地图：" + (mapList.TestMap != null ? mapList.TestMap.gameObject.name : "默认"), style);
        //EditorGUILayout.Space ();
        //selectIndex = EditorGUILayout.Popup ("EditableMaps", selectIndex, mapsNameArr.ToArray ());
        //GUILayout.BeginHorizontal ();
        //if (GUILayout.Button ("编辑")) {
        //    EditMap (mapsNameArr [selectIndex]);
        //}
        //if (GUILayout.Button ("设为开始地图")) {
        //    ToolsHelperEditor.SetStartupScene (mapsNameArr [selectIndex]);
        //}
        //if (GUILayout.Button ("删除")) {
        //    if (EditorUtility.DisplayDialog ("防手抖", "注意要删除的地图名为\"" + mapsNameArr [selectIndex] + "\"", "确定删除", "取消")) {
        //        DeleteMap (mapsNameArr [selectIndex]);
        //    }
        //}
        //GUILayout.EndHorizontal ();
        EditorGUILayout.Space ();
        GUILayout.BeginHorizontal ();
        inputMapName = EditorGUILayout.TextField ("NewCollectItem", inputMapName);
        if (GUILayout.Button ("创建")) {
            CreateNewMap (inputMapName);
        }
        GUILayout.EndHorizontal ();
    }

    private List<string> UpdateAllEditableMapsName () {
        mapsNameArr = new List<string> ();
        string [] allPath = AssetDatabase.FindAssets ("t:Prefab", new string [] { "Assets/Resources/CollectItems" });
        for (int i = 0; i < allPath.Length; i++) {
            string path = AssetDatabase.GUIDToAssetPath (allPath [i]);
            GameObject go = AssetDatabase.LoadAssetAtPath<GameObject> (path);
            if (go != null && go.GetComponent<Biz.Item.Item> () != null) {
                mapsNameArr.Add (go.name);
            }
        }
        return mapsNameArr;
    }

    private void SetCurrentSceneIndex () {
        UpdateAllEditableMapsName ();
        string currentSceneName = EditorSceneManager.GetActiveScene ().name;
        for (int i = 0; i < mapsNameArr.Count; i++) {
            if (currentSceneName == "Edit" + mapsNameArr [i]) {
                selectIndex = i;
                break;
            }
        }
    }

    private void EditMap (string mapName) {
        string sceneName = "CollectItem_" + mapName;
        string scenePath = "Assets/Scenes/" + sceneName + ".unity";
        if (EditorSceneManager.GetActiveScene ().name != sceneName) {
            SceneAsset scene = AssetDatabase.LoadAssetAtPath<SceneAsset> (scenePath);
            if (scene == null) {
                EditorUtility.DisplayDialog ("提示", "找不到场景文件" + scenePath, "好的");
                return;
            } else EditorSceneManager.OpenScene (scenePath);
        }
        GameObject mapGo = GameObject.FindObjectOfType<Biz.Item.Item> ()?.gameObject;
        if (mapGo == null) {
            EditorUtility.DisplayDialog ("提示", "该场景中找不到地图", "好的");
            return;
        }
        Selection.activeGameObject = mapGo;
        //SetCurrentSceneIndex ();
    }

    private void DeleteMap (string mapName) {
        string mapPath = "Assets/Resources/CollectItems/" + mapName + ".prefab";
        string sceneName = "CollectItem_" + mapName;
        string scenePath = "Assets/Scenes/" + sceneName + ".unity";
        AssetDatabase.DeleteAsset (mapPath);
        AssetDatabase.DeleteAsset (scenePath);
        AssetDatabase.Refresh ();
        selectIndex = 0;
        if (EditorSceneManager.GetActiveScene ().name == sceneName) {
            EditorSceneManager.OpenScene (mainScenePath);
        }
    }

    private void CreateNewMap (string mapName) {
        string mapPath = "Assets/Resources/CollectItems/" + mapName + ".prefab";
        string sceneName = "CollectItem_" + mapName;
        string scenePath = "Assets/Scenes/" + sceneName + ".unity";
        AssetDatabase.CopyAsset (templetePath, mapPath);
        AssetDatabase.CopyAsset (editMapTempletePath, scenePath);
        AssetDatabase.Refresh ();
        EditorSceneManager.OpenScene (scenePath);

        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject> (mapPath);
        GameObject gameObject = PrefabUtility.InstantiatePrefab (prefab) as GameObject;
        if (Selection.gameObjects.Length > 0) {
            gameObject.transform.SetParent (Selection.gameObjects [0].transform, false);
        }
        Selection.activeGameObject = gameObject;
        gameObject.name = mapName;
        EditorSceneManager.SaveScene (gameObject.scene, scenePath);
        EditMap (mapName);
    }

}