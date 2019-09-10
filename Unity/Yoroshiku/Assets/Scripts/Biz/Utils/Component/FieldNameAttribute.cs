using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class FieldNameAttribute : PropertyAttribute {
    public string Title;
    public FieldNameAttribute(string title) {
        this.Title = title;
    }

}