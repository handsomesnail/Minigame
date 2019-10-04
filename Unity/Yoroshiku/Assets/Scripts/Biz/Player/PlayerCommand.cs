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

    /// <summary>进入溶入区域</summary>
    public class EnterMeltAreaCommand : PlayerCommand {
        public MeltArea MeltArea { get; private set; }
        public EnterMeltAreaCommand(MeltArea meltArea) {
            this.MeltArea = meltArea;
        }
    }

    /// <summary>离开溶入区域</summary>
    public class ExitMeltAreaCommand : PlayerCommand {
        public MeltArea MeltArea { get; private set; }
        public ExitMeltAreaCommand(MeltArea meltArea) {
            this.MeltArea = meltArea;
        }
    }

    /// <summary>设置(如果)触发溶出时的区域(保留溶出前的MeltArea用于计算溶出推力方向)</summary>
    public class MeltOutCommand : PlayerCommand {
        public MeltArea MeltArea { get; private set; }
        public MeltOutCommand(MeltArea meltArea) {
            this.MeltArea = meltArea;
        }
    }

    public class GetPlayerDataCommand : PlayerCommand { }

    public class SpringPushForceCommand : PlayerCommand {
        public Vector2 Force { get; private set; }
        public SpringPushForceCommand(Vector2 force) {
            this.Force = force;
        }
    }

    /// <summary>死亡区域触发</summary>
    public class DeadAreaTriggerCommand : PlayerCommand { }

    /// <summary>设置当前停留的地面碰撞体(用于计算移动方向)</summary>
    public class SetStayedGroundCommand : PlayerCommand {
        public Collider2D Ground;
        public SetStayedGroundCommand(Collider2D ground) {
            this.Ground = ground;
        }
    }

    public class ShowItemTipCommand : PlayerCommand {
        public Biz.Item.Item Item;
        public ShowItemTipCommand(Biz.Item.Item item) {
            this.Item = item;
        }
    }

}