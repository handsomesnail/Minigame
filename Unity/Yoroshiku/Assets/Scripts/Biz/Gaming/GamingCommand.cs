using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Gaming {
    public abstract class GamingCommand : Command {
        public override Type GetController() {
            return typeof(GamingController);
        }
    }

    public class ShowCommand : GamingCommand {
        public int MapIndex { get; private set; }
        public ShowCommand(int mapIndex) {
            this.MapIndex = mapIndex;
        }
    }

    public class CloseCommand : GamingCommand { }

}