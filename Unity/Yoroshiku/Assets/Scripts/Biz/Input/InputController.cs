using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Gaming;
using Biz.Player;
using UnityEngine;
using ZCore;

namespace Biz.Input {

    public sealed class InputController : Controller<GamingModel, GamingView> {
        public void OnInitCommand(InitCommand cmd) {
            //根据Model.Map.AvailableColors显示颜色
        }

        //Editor输入
        private void Update() {
            //跳跃
            if (UnityEngine.Input.GetButtonDown("Jump")) {
                Call(new JumpCommand());
            }
            //溶入
            if (UnityEngine.Input.GetKeyDown(KeyCode.J)) {
                Call(new MeltCommand());
            }
        }

        private void FixedUpdate() {
            //移动
            Vector2 offset = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
            Call(new MoveCommand(offset));
        }

        //TODO: UI输入

    }
}