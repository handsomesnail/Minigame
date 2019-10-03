using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Player {

    [CreateAssetMenu(menuName = "ScriptableObject/PlayerAudioSetting")]
    public class PlayerAudioSetting : ScriptableObject {

        [FieldName("跳跃音效")]
        public AudioClip JumpAudioClip;

        [FieldName("溶入音效")]
        public AudioClip MeltInAudioClip;

        [FieldName("溶出音效")]
        public AudioClip MeltOutAudioClip;

    }
}