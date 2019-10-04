using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Loading {
    public abstract class LoadingCommand : Command {
        public override Type GetController() {
            return typeof(LoadingController);
        }
    }

    /// <summary>播放过渡动画(可指定过渡中执行的方法)</summary>
    public class TransitCommand : LoadingCommand {
        public Action TransitionCallback { get; private set; } //同步回调方法
        public IEnumerator TransitionAsyncCallback { get; private set; } //异步回调方法

        public bool ClearResource{get; private set;} //是否清理资源
        public TransitCommand() { }
        public TransitCommand(Action transitionCallback, bool isClear) {
            this.TransitionCallback = transitionCallback;
            this.ClearResource = isClear;
        }

        public TransitCommand(IEnumerator transitionAsyncCallback, bool isClear) {
            this.TransitionAsyncCallback = transitionAsyncCallback;
            this.ClearResource = isClear;
        }

    }

}