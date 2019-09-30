using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Map {

    /// <summary>触发类型</summary>
    public enum TriggerType {
        Enter = 0, //进入触发
        Exit //离开触发
    }

    public abstract class BaseTrigger : CallerBehaviour {

        /// <summary>当前是否已触发(由子类定义)</summary>
        public bool Triggered = false;

        /// <summary>是否仅触发一次</summary>
        public bool OnlyOnce;

        /// <summary>触发</summary>
        protected abstract void Trigger(TriggerType type);

        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.name == "MoveCheckerCollider") {
                if (Triggered && OnlyOnce) {
                    return;
                }
                Trigger(TriggerType.Enter);
            }
        }

        private void OnTriggerExit2D(Collider2D collider) {
            if (collider.gameObject.name == "MoveCheckerCollider") {
                if (Triggered && OnlyOnce) {
                    return;
                }
                Trigger(TriggerType.Exit);
            }
        }

    }
}