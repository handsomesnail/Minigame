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

    [MenuItem("GameObject/Yoroshiku/障碍物(四边形)", false, 1)]
    public static void CreateNewBarrier() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/Barrier.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/障碍物(多边形)", false, 1)]
    public static void CreateNewPolygonBarrier() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/PolygonBarrier.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/障碍物(边)", false, 1)]
    public static void CreateNewEdgeBarrier() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/EdgeBarrier.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/溶入区域", false, 2)]
    public static void CreateNewMeltArea() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/MeltArea.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/弹簧", false, 3)]
    public static void CreateSpring() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/Spring.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/钟摆", false, 4)]
    public static void CreateNewPendulum() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/Pendulum.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/死亡区域", false, 5)]
    public static void CreateNewDeadArea() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/DeadArea.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/存档点", false, 6)]
    public static void CreateNewStorage() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/Storage.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/过关点", false, 7)]
    public static void CreateNewPassPoint() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/PassPoint.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/剧情&引导", false, 8)]
    public static void CreateNewGuideTrigger() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/GuideTrigger.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/动画触发区域", false, 9)]
    public static void CreateNewAnimTrigger() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/AnimTrigger.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/收集品", false, 10)]
    public static void CreateNewCollectItem() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/CollectItem.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/音效触发区域", false, 11)]
    public static void CreateNewAudioTrigger() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/AudioTrigger.prefab");
    }

    [MenuItem("GameObject/Yoroshiku/纸飞机", false, 12)]
    public static void CreateNewPaperPlane() {
        PlaceYoroshikuElement("Assets/Prefabs/MapObjects/PaperPlane.prefab");
    }

    private static void PlaceYoroshikuElement(string prefabPath) {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        GameObject gameObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        if (Selection.gameObjects.Length > 0) {
            gameObject.transform.SetParent(Selection.gameObjects[0].transform, false);
        }
        Selection.activeGameObject = gameObject;
    }

    public static void SetStartupScene(string mapName) {
        MapList mapList = AssetDatabase.LoadAssetAtPath<MapList>("Assets/Resources/Configs/MapList.asset");
        string mapPath = "Assets/Prefabs/Maps/" + mapName + ".prefab";
        GameObject mapPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(mapPath);
        mapList.TestMap = mapPrefab.GetComponent<BaseMap>();
        EditorUtility.SetDirty(mapList);
        AssetDatabase.SaveAssets();
    }

}