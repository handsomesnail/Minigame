using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Biz.Utils {
    public static class Util {
        public static long GetTimeSpan(Action action) {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public static void Active(this GameObject gameObject, bool active) {
            if (gameObject.activeSelf != active) {
                gameObject.SetActive(active);
            }
        }

    }
}