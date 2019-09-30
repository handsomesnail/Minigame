using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Map {
    public class ContentTrigger : BaseTrigger {

        /// <summary>引导或剧情的内容(进入显示离开消失)</summary>
        public GameObject Content;

        private Collider2D EnterCheckCollider;

        private Collider2D ExitCheckCollider;

        private void Awake() {
            Collider2D[] colliders = GetComponents<Collider2D>();
            EnterCheckCollider = colliders[0];
            ExitCheckCollider = colliders[1];
        }
        private void Start() {
            Content.SetActive(false);
            EnterCheckCollider.enabled = true;
            ExitCheckCollider.enabled = false;
        }

        protected override void Trigger(TriggerType type) {
            EnterCheckCollider.enabled = type == TriggerType.Exit;
            ExitCheckCollider.enabled = type == TriggerType.Enter;
            Content.SetActive(type == TriggerType.Enter);
            //离开的时候算触发一次
            if (type == TriggerType.Exit) {
                Triggered = true;
            }
        }

    }

}