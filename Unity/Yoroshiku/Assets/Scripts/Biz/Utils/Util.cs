using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Biz.Utils {
    public static class Util {
        public static long GetTimeSpan(Action action) {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}