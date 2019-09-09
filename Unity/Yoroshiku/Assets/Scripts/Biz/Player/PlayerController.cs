using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Gaming;
using Biz.Map;
using UnityEngine;
using ZCore;

namespace Biz.Player {
    public sealed class PlayerController : Controller<GamingModel, GamingView> {

        public void OnInitCommand(InitCommand cmd) {
            View.PlayerView.Player.transform.position = Model.Map.BornPoint.position; //设置Player的出生点
            //设置Player的初始数据
            Model.CurrentColorIndex = 0;
            Model.MeltStatus = false;
            Model.Offset = Vector2.zero;
            Model.Jump = false;
            Model.CurrentStayMeltArea = null;
        }

        public void OnMoveCommand(MoveCommand cmd) {
            Model.Offset = cmd.Offset;
        }

        public void OnJumpCommand(JumpCommand cmd) {
            //非溶入状态且在地面设置跳跃标志位
            if (IsGroundByCollider() && !Model.MeltStatus) {
                Model.Jump = true;
            }
        }

        public void OnMeltCommand(MeltCommand cmd) {
            if (IsMeltAvaliable()) {
                SetMeltStatus(!Model.MeltStatus);
            }
        }

        public void OnEnterMeltAreaCommand(EnterMeltAreaCommand cmd) {
            Model.CurrentStayMeltArea = cmd.MeltArea;
        }

        public void OnExitMeltAreaCommand(ExitMeltAreaCommand cmd) {
            Model.CurrentStayMeltArea = null;
        }

        private void FixedUpdate() {
            if (Model.GameStatus == GameStatus.Gaming) {
                UpdatePlayer();
            }
        }

        private void UpdatePlayer() {
            Rigidbody2D rigidbody = View.PlayerView.Rigidbody;
            float maxSpeed = Model.Map.PlayerConfigData.moveSpeed;
            float jumpForce = Model.Map.PlayerConfigData.jumpForce;
            rigidbody.velocity = new Vector2(Model.Offset.x * maxSpeed, Model.MeltStatus? Model.Offset.y * maxSpeed : rigidbody.velocity.y);
            if (Model.Jump) {
                rigidbody.AddForce(new Vector2(0f, jumpForce));
                Model.Jump = false;
            }
            if (Model.MeltStatus && !IsMeltAvaliable()) {
                SetMeltStatus(false);
            }
        }

        /// <summary>当前和MeltArea相交且颜色对应正确</summary>
        private bool IsMeltAvaliable() {
            return Model.CurrentStayMeltArea != null && Model.CurrentStayMeltArea.ColorIndex == Model.CurrentColorIndex;
        }

        //TODO : 应该用碰撞体检测或者多加几个检测点
        private bool IsGround() {
            return Physics2D.Linecast(View.PlayerView.Player.transform.position, View.PlayerView.GroundChecker.position, 1 << LayerMask.NameToLayer("Ground"));
        }

        private bool IsGroundByCollider() {
            return View.PlayerView.GroundCheckCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        }

        private void SetMeltStatus(bool meltStatus) {
            Model.MeltStatus = meltStatus;
            View.PlayerView.NormalEntity.SetActive(!Model.MeltStatus);
            View.PlayerView.MeltedEntity.SetActive(Model.MeltStatus);
            Rigidbody2D rigidbody = View.PlayerView.Rigidbody;
            if (Model.MeltStatus) {
                rigidbody.velocity = Vector2.zero;
                rigidbody.gravityScale = 0;
            }
            else {
                rigidbody.gravityScale = 1;
            }
        }

    }
}