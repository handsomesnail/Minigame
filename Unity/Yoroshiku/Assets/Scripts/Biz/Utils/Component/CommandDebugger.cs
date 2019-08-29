using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Utils.Component {
    public class CommandDebugger : CallerBehaviour {
        public string cmdFullName;
        public void Call() {
            Call(cmdFullName);
        }

    }
}