using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Gaming;
using UnityEngine;
using ZCore;

namespace Biz.Map {

    // /// <summary>触发类型</summary>
    // public enum TriggerType {
    //     Enter = 0, //进入触发
    //     Exit //离开触发
    // }

    [Serializable]
    public class AnimTrigger {
        /// <summary>触发摄像机动画</summary>
        public AnimationClip CameraAnimClip;

        /// <summary>触发动画</summary>
        public Animation[] TriggerAnims;

        /// <summary>关闭动画</summary>
        public Animation[] StopAnims;
    }

    /// <summary>新手引导或剧情触发器</summary>
    [RequireComponent(typeof(Collider2D))]
    public class GuideTrigger : CallerBehaviour {

        /// <summary>引导或剧情的内容(进入显示离开消失)</summary>
        public GameObject Content;

        public AnimTrigger EnterAnimTrigger;

        public AnimTrigger ExitAnimTrigger;
        private void OnTriggerEnter2D(Collider2D collider) {
            Content.SetActive(true);
            if (collider.gameObject.name == "MoveCheckerCollider") {
                Trigger(EnterAnimTrigger);
            }
        }

        private void OnTriggerExit2D(Collider2D collider) {
            Content.SetActive(false);
            if (collider.gameObject.name == "MoveCheckerCollider") {
                Trigger(ExitAnimTrigger);
            }
        }

        private void Trigger(AnimTrigger animTrigger) {
            if (animTrigger.CameraAnimClip != null) {
                Call(new CameraAnimCommand(animTrigger.CameraAnimClip));
            }
            foreach (var anim in animTrigger.TriggerAnims) {
                if (!anim.isPlaying) {
                    anim.Play();
                }
            }
            foreach (var anim in animTrigger.StopAnims) {
                if (anim.isPlaying) {
                    anim.Stop();
                }
            }
        }

    }
}