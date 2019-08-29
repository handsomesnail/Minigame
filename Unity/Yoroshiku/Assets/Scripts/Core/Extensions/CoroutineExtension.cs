using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class CoroutineExtension {

    public static IEnumerator Wait(YieldInstruction yieldInstruction, Action completeCallback) {
        yield return yieldInstruction;
        completeCallback.CheckedInvoke();
    }

    public static IEnumerator WaitUntil(Func<bool> predicate, Action completeCallback) {
        yield return new WaitUntil(predicate);
        completeCallback.CheckedInvoke();
    }

    public static IEnumerator WaitCompleted(this Task task) {
        yield return new WaitUntil(() => { return task.IsCompleted; });
    }
    public static IEnumerator WaitCompleted<TResult>(this Task<TResult> task, Action<TResult> completeCallback) {
        yield return new WaitUntil(() => { return task.IsCompleted; });
        completeCallback.CheckedInvoke(task.Result);
    }

    public static async Task Wait(int millisecondsTimeout) {
        Task task = Task.Run(() => { });
        task.Wait(millisecondsTimeout);
        await task;
    }

    public static IEnumerator Wait(float seconds) {
        yield return new WaitForSeconds(seconds);
    }

}