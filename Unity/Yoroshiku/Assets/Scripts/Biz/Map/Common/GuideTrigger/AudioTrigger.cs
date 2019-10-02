using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Gaming;
using UnityEngine;
using ZCore;

namespace Biz.Map {
    public class AudioTrigger : BaseTrigger {

        public TriggerType Type;

        public AudioSource PlayAudio;

        public AudioSource StopAudio;

        protected override void Trigger(TriggerType type) {
            if (type != Type) {
                return;
            }
            if (PlayAudio != null) {
                PlayAudio.Play();
            }
            if (StopAudio != null) {
                StopAudio.Stop();
            }
            Triggered = true;
        }
    }
}