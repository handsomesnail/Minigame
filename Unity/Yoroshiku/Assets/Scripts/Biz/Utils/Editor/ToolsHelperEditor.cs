using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Map;
using Biz.Player;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public static class ToolsHelperEditor {

    [MenuItem("Yoroshiku/角色基本属性")]
    public static void EditPlayerSetting() {
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<PlayerSetting>("Assets/Resources/Configs/DefaultPlayerSetting.asset");
    }

    [MenuItem("Yoroshiku/地图编辑窗口")]
    public static void EditMapList() {
        MapEditorWindow window = EditorWindow.GetWindow<MapEditorWindow>();
        window.Show();
    }

    [MenuItem("GameObject/Yoroshiku/地面", false, 0)]
    public static void CreateNewGround() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/Ground.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/障碍物", false, 1)]
    public static void CreateNewBarrier() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/Barrier.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/溶入区域", false, 2)]
    public static void CreateNewMeltArea() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/MeltArea.prefab");
    }

    private static void PlaceYoroshikuElement(string prefabPath) {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        GameObject gameObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        if (Selection.gameObjects.Length > 0) {
            gameObject.transform.SetParent(Selection.gameObjects[0].transform, false);
        }
        Selection.activeGameObject = gameObject;
    }

}