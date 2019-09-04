using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Input;
using Biz.Map;
using Biz.Player;
using UnityEngine;
using ZCore;

namespace Biz.Gaming {
    public sealed class GamingController : Controller<GamingModel, GamingView> {

        public void OnEnterCommand(EnterCommand cmd) {
            Model.MapIndex = cmd.MapIndex;
            Model.GameStatus = GameStatus.Gaming;
            //先加载地图 获取地图数据(必须先执行该Command)
            Call(new Biz.Map.LoadCommand());
            //初始化Player
            Call(new Biz.Player.InitCommand());
            //初始化UI开始接收输入
            Call(new Biz.Input.InitCommand());
        }

        public void OnExitCommand(ExitCommand cmd) {
            Model.GameStatus = GameStatus.None;
            View.Destroy();
        }

        public void OnPauseCommand(PauseCommand cmd) {
            Model.GameStatus = GameStatus.Pause;
        }

        public void OnResumeCommand(ResumeCommand cmd) {
            Model.GameStatus = GameStatus.Gaming;
        }

    }
}