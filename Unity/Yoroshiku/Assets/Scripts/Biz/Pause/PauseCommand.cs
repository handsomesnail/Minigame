using System;
using ZCore;
using UnityEngine;
namespace Biz.Pause {
    public class PauseCommand: Command {
        public override Type GetController () {
            return typeof (PauseController);
        }
    }
}
