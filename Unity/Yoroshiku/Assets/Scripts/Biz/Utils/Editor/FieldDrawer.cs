using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FieldNameAttribute))]
public class FieldLabelDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        label.text = (attribute as FieldNameAttribute).Title;
        EditorGUI.PropertyField(position, property, label, true);
    }
}