using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Player;
using UnityEditor;
using UnityEngine;

public static class ToolsHelperEditor {

    [MenuItem("Yoroshiku/角色基本属性")]
    public static void EditPlayerSetting() {
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<PlayerSetting>("Assets/Resources/Configs/DefaultPlayerSetting.asset");
    }

    [MenuItem("Yoroshiku/创建新地图")]
    public static void CreateMapScene() {
        AssetDatabase.Refresh();
    }

}