using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Biz.Player;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerSetting))]
public class PlayerSettingEditor : Editor {
    public override void OnInspectorGUI() {
        base.DrawDefaultInspector();
        EditorGUILayout.Space();
        PlayerSetting playerSetting = (PlayerSetting) target;
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 12;
        GUILayout.Box(GUIContent.none, GUI.skin.box, GUILayout.ExpandWidth(true), GUILayout.Height(1f));
        float Normal_MoveToMaxSpeedDuration = playerSetting.Normal_MaxMoveSpeed / playerSetting.Normal_MoveForce;
        float Melted_MoveToMaxSpeedDuration = playerSetting.Melted_MaxMoveSpeed / playerSetting.Melted_MoveForce;
        float Air_MoveToMaxSpeedDuration = playerSetting.Air_MaxMoveSpeed / playerSetting.Air_MoveForce;
        EditorGUILayout.LabelField(string.Format("移动加速到最大耗时    普通：{0:F2}s    溶入：{1:F2}s    空中：{2:F2}s",
            Normal_MoveToMaxSpeedDuration, Melted_MoveToMaxSpeedDuration, Air_MoveToMaxSpeedDuration), style);
        float Normal_MaxSpeedDampDuration = playerSetting.Normal_MaxMoveSpeed / playerSetting.Normal_LinearDrag;
        float Melted_MaxSpeedDampDuration = playerSetting.Melted_MaxMoveSpeed / playerSetting.Melted_LinearDrag;
        float Air_MaxSpeedDampDuration = playerSetting.Air_MaxMoveSpeed / playerSetting.Air_LinearDrag;
        EditorGUILayout.LabelField(string.Format("无输入速度衰减耗时    普通：{0:F2}s    溶入：{1:F2}s    空中：{2:F2}s",
            Normal_MaxSpeedDampDuration, Melted_MaxSpeedDampDuration, Air_MaxSpeedDampDuration), style);
        double jumpHeight = Math.Pow(playerSetting.JumpInitialSpeed, 2) / (2 * (playerSetting.Gravity));
        EditorGUILayout.LabelField(string.Format("跳跃高度： {0:F2}m", jumpHeight), style);
        float jumpUpDuration = playerSetting.JumpInitialSpeed / (playerSetting.Gravity);
        EditorGUILayout.LabelField(string.Format("跳跃垂直上升耗时： {0:F2}s", jumpUpDuration), style);
        float jumpDropDuration = playerSetting.JumpInitialSpeed / (playerSetting.Gravity);
        EditorGUILayout.LabelField(string.Format("跳跃垂直下落耗时： {0:F2}s", jumpDropDuration), style);

    }

}