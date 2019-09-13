using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Map;
using Biz.Player;
using UnityEditor;
using UnityEngine;

public static class ToolsHelperEditor {

    [MenuItem("Yoroshiku/角色基本属性")]
    public static void EditPlayerSetting() {
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<PlayerSetting>("Assets/Resources/Configs/DefaultPlayerSetting.asset");
    }

    [MenuItem("Yoroshiku/编辑地图列表")]
    public static void CreateMapScene() {
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<MapList>("Assets/Resources/Configs/MapList.asset");
    }

}