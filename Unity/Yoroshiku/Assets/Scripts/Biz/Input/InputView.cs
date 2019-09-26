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
        public Button MeltBtn;
        public Button JumpBtn;
        public Button PauseBtn;
    }
}