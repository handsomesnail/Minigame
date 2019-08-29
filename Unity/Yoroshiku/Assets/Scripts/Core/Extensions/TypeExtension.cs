using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TypeExtension {

    /// <summary>判断某类型是否继承自某开放类型</summary>
    public static bool IsSubClassOfRawGeneric(this Type type, Type genericType) {
        if (type == null || genericType == null) {
            throw new ArgumentNullException();
        }
        while (type != null && type != typeof(object)) {
            if ((type.IsGenericType ? type.GetGenericTypeDefinition() : type) == genericType) {
                return true;
            }
            type = type.BaseType;
        }
        return false;
    }

}