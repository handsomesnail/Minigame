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

    /// <summary>进入游戏</summary>
    public class EnterCommand : GamingCommand {
        public int MapIndex { get; private set; }

        public bool TryUseTestMap { get; private set; }

        public EnterCommand() {
            TryUseTestMap = true;
        }

        public EnterCommand(int mapIndex) {
            this.TryUseTestMap = false;
            this.MapIndex = mapIndex;
        }
    }

    /// <summary>离开游戏</summary>
    public class ExitCommand : GamingCommand { }

    /// <summary>暂停</summary>
    public class PauseCommand : GamingCommand { }

    /// <summary>恢复</summary>
    public class ResumeCommand : GamingCommand { }

}