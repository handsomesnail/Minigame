using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Gaming;
using UnityEngine;
using ZCore;

namespace Biz.Map {
    public class AnimTrigger : BaseTrigger {

        public TriggerType Type;

        /// <summary>触发摄像机动画</summary>
        public AnimationClip CameraAnimClip;

        /// <summary>触发动画</summary>
        public Animation[] TriggerAnims;

        /// <summary>关闭动画</summary>
        public Animation[] StopAnims;

        protected override void Trigger(TriggerType type) {
            if (type != Type) {
                return;
            }
            if (CameraAnimClip != null) {
                Call(new CameraAnimCommand(CameraAnimClip));
            }
            foreach (var anim in TriggerAnims) {
                if (!anim.isPlaying) {
                    anim.Play();
                }
            }
            foreach (var anim in StopAnims) {
                if (anim.isPlaying) {
                    anim.Stop();
                }
            }
            Triggered = true;
        }
    }
}