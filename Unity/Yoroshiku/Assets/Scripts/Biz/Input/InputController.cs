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
            View.InputView.Joystick.onMove.AddListener(OnJoyStickMove);
            View.InputView.JumpBtn.onDown.AddListener(OnClickDownJumpBtn);
            View.InputView.MeltBtn.onDown.AddListener(OnClickDownMeltBtn);
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

        //UI输入
        private void OnJoyStickMove(Vector2 offset) {
            Call(new MoveCommand(offset));
        }

        private void OnClickDownJumpBtn() {
            Call(new JumpCommand());
        }
        private void OnClickDownMeltBtn() {
            Call(new MeltCommand());
        }

    }
}