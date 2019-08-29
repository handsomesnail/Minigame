using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Utils.Component;
using UnityEditor;
using UnityEngine;
using ZCore;

[CustomEditor(typeof(CommandDebugger))]
public class CommandDebuggerEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        CommandDebugger script = (CommandDebugger) target;
        if (GUILayout.Button("Call")) {
            script.Call();
        }
    }

}