using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZCore;

namespace Biz.Input {

    [Serializable]
    public class InputView {
        public ETCJoystick Joystick;
        public ETCButton MeltBtn;
        public ETCButton JumpBtn;
        public ETCButton PauseBtn;
        public AnimationCurve MoveCurve;
    }
}