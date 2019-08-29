using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DelegateExtension {
    public static void CheckedInvoke(this Action action) {
        action?.Invoke();
    }
    public static void CheckedInvoke<T>(this Action<T> action, T parameter) {
        action?.Invoke(parameter);
    }
    public static void CheckedInvoke<T1, T2>(this Action<T1, T2> action, T1 parameter1, T2 parameter2) {
        action?.Invoke(parameter1, parameter2);
    }

    public static TResult CheckedInvoke<TResult>(this Func<TResult> func) {
        if (func != null)
            return func.Invoke();
        else return default(TResult);
    }
    public static TResult CheckedInvoke<T, TResult>(this Func<T, TResult> func, T parameter) {
        if (func != null)
            return func.Invoke(parameter);
        else return default(TResult);
    }
    public static TResult CheckedInvoke<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 parameter1, T2 parameter2) {
        if (func != null)
            return func.Invoke(parameter1, parameter2);
        else return default(TResult);
    }
}