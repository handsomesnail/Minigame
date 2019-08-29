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

        public void OnShowCommand(ShowCommand cmd) {
            Model.MapIndex = cmd.MapIndex;
            //先加载地图 获取地图数据(必须先执行该Command)
            Call(new Biz.Map.LoadCommand());
            //初始化Player
            Call(new Biz.Player.InitCommand());
            //初始化UI开始接收输入
            Call(new Biz.Input.InitCommand());
        }

        public void OnCloseCommand(CloseCommand cmd) {
            Destroy(View.gameObject);
        }
    }
}