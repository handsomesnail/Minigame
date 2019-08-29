using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Map;
using UnityEngine;
using ZCore;

namespace Biz.Player {
    public abstract class PlayerCommand : Command {
        public override Type GetController() {
            return typeof(PlayerController);
        }
    }

    public class InitCommand : PlayerCommand { }

    /// <summary>移动</summary>
    public class MoveCommand : PlayerCommand {
        public Vector2 Offset { get; private set; }
        public MoveCommand(Vector2 offset) {
            this.Offset = offset;
        }
    }

    /// <summary>跳跃</summary>
    public class JumpCommand : PlayerCommand { }

    /// <summary>溶入(出)</summary>
    public class MeltCommand : PlayerCommand { }

    /// <summary>切换颜色(AvailableColors[index]即选中的颜色)</summary>
    public class SwitchColorCommand : PlayerCommand {
        public int ColorIndex { get; private set; }
        public SwitchColorCommand(int colorIndex) {
            this.ColorIndex = colorIndex;
        }
    }

    public class EnterMeltAreaCommand : PlayerCommand {
        public MeltArea MeltArea { get; private set; }
        public EnterMeltAreaCommand(MeltArea meltArea) {
            this.MeltArea = meltArea;
        }
    }

    public class ExitMeltAreaCommand : PlayerCommand {
        public MeltArea MeltArea { get; private set; }
        public ExitMeltAreaCommand(MeltArea meltArea) {
            this.MeltArea = meltArea;
        }
    }

}