using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ZCore;

namespace Biz.Map {
    public class ContentTrigger : BaseTrigger {

        /// <summary>引导或剧情的内容(进入显示离开消失)</summary>
        public GameObject Content;

        public Text ContentText;

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
            Action completeTrigger = () => Content.SetActive(type == TriggerType.Enter);
            if (type == TriggerType.Enter) {
                Color c = ContentText.color;
                ContentText.color = new Color(c.r, c.g, c.b, 0);
                Content.SetActive(true);
                ContentText.DOFade(1, 0.5f);
            }
            else {
                ContentText.DOFade(0, 0.5f).OnComplete(() => {
                    Content.SetActive(false);
                });
            }

            //离开的时候算触发一次
            if (type == TriggerType.Exit) {
                Triggered = true;
            }
        }

    }

}