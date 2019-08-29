using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Input {
    public abstract class InputCommand : Command {
        public override Type GetController() {
            return typeof(InputController);
        }
    }

    public class InitCommand : InputCommand { }

}