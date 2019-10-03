using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Gaming;
using UnityEngine;
using ZCore;

namespace Biz.Map {
    [Serializable]
    public struct AnimMsg {
        public Animator Anim;
        public string State;
        public bool KillSelf;
    }
    public class AnimTrigger : BaseTrigger {

        public TriggerType Type;

        /// <summary>触发摄像机动画</summary>
        public AnimationClip CameraAnimClip;

        /// <summary>触发动画</summary>
        public Animation[] TriggerAnims;

        /// <summary>关闭动画</summary>
        public Animation[] StopAnims;

        public AnimMsg[] AnimMsgs;

        protected override void Trigger(TriggerType type) {
            if (type != Type) {
                return;
            }
            if (CameraAnimClip != null) {
                Call(new CameraAnimCommand(CameraAnimClip));
            }
            foreach (var anim in TriggerAnims) {
                if (anim != null && !anim.isPlaying) {
                    anim.Play();
                }
            }
            foreach (var anim in StopAnims) {
                if (anim != null && anim.isPlaying) {
                    anim.Stop();
                }
            }
            foreach (var msg in AnimMsgs) {
                if (msg.Anim != null && msg.Anim.gameObject != null) {
                    msg.Anim.Play(msg.State);
                    if (msg.KillSelf) {
                        AnimKiller killer = msg.Anim.gameObject.AddComponent<AnimKiller>();
                        killer.StateName = msg.State;
                    }
                }
            }
            Triggered = true;
        }
    }
}