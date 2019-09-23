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
            Model.LastJumpReqTime = float.MinValue;
            Model.LastMeltReqTime = float.MinValue;
        }

        public void OnMoveCommand(MoveCommand cmd) {
            Model.Offset = cmd.Offset;
        }

        public void OnJumpCommand(JumpCommand cmd) {
            //非溶入状态且在地面设置跳跃标志位
            if (IsGroundByCollider() && !Model.MeltStatus) {
                Model.Jump = true;
            }
            else {
                Model.LastJumpReqTime = Time.fixedTime;
            }
        }

        public void OnMeltCommand(MeltCommand cmd) {
            if (IsMeltAvaliable()) {
                SetMeltStatus(!Model.MeltStatus);
            }
            else if (!Model.MeltStatus) {
                Model.LastMeltReqTime = Time.fixedTime;
            }
        }

        public void OnEnterMeltAreaCommand(EnterMeltAreaCommand cmd) {
            Model.CurrentStayMeltArea = cmd.MeltArea;
        }

        public void OnExitMeltAreaCommand(ExitMeltAreaCommand cmd) {
            Model.CurrentStayMeltArea = null;
        }

        public PlayerData OnGetPlayerDataCommand(GetPlayerDataCommand cmd) {
            return new PlayerData() {
                ColorIndex = Model.CurrentColorIndex,
                    MeltStatus = Model.MeltStatus,
            };
        }

        private void FixedUpdate() {
            if (Model.GameStatus == GameStatus.Gaming) {
                UpdatePlayer();
            }
        }

        private void UpdatePlayer() {
            Debug.Log(View.PlayerView.Player.position);
            PlayerSetting playerSetting = View.PlayerSetting;
            Rigidbody2D rigidbody = View.PlayerView.Rigidbody;

            //溶入请求计时时间内如果可溶入且状态为非溶入 则进行溶入操作
            if (Time.fixedTime - Model.LastMeltReqTime < playerSetting.MeltJudgeDuration && !Model.MeltStatus && IsMeltAvaliable()) {
                SetMeltStatus(true);
            }
            //当前是溶入状态且离开了可溶入区域 则进行溶出操作
            if (Model.MeltStatus && !IsMeltAvaliable()) {
                SetMeltStatus(false);
            }

            //--配置--
            float maxMoveSpeed = 0;
            float moveForce = 0;
            float linearDrag = 0;
            float gravity = 0;
            //--配置--
            Vector2 moveForceDirection = Vector2.zero; //有效移动输入(最后会转为移动动力的输入)
            #region 根据当前状态设置上述配置参数和数据
            if (!Model.MeltStatus) {
                //普通状态在地面
                if (IsGroundByCollider()) {
                    moveForce = playerSetting.Normal_MoveForce;
                    maxMoveSpeed = playerSetting.Normal_MaxMoveSpeed;
                    linearDrag = playerSetting.Normal_LinearDrag;
                    gravity = playerSetting.Gravity;
                }
                //普通状态在空中
                else {
                    moveForce = playerSetting.Air_MoveForce;
                    maxMoveSpeed = playerSetting.Air_MaxMoveSpeed;
                    linearDrag = playerSetting.Air_LinearDrag;
                    //TODO：有点问题
                    //修正重力加速度抵消空中垂直摩擦力，仅有水平摩擦力生效
                    //目的是让垂直加速度的控制仅受Gravity一个参数影响
                    if (rigidbody.velocity.y > 0) {
                        gravity = playerSetting.Gravity - linearDrag;
                    }
                    else {
                        gravity = playerSetting.Gravity + linearDrag;
                    }
                }
                moveForceDirection = new Vector2(GetValidInput(Model.Offset.x, rigidbody.velocity.x, maxMoveSpeed), 0);
            }
            //溶入状态
            else {
                moveForce = playerSetting.Melted_MoveForce;
                maxMoveSpeed = playerSetting.Melted_MaxMoveSpeed;
                linearDrag = playerSetting.Melted_LinearDrag;
                gravity = 0;
                moveForceDirection = GetValidInput(Model.Offset, rigidbody.velocity, new Vector2(maxMoveSpeed, maxMoveSpeed));
            }
            #endregion

            rigidbody.gravityScale = gravity / (-1 * Physics2D.gravity.y);
            rigidbody.drag = linearDrag * rigidbody.mass;
            rigidbody.AddForce(moveForceDirection * (moveForce + linearDrag) * rigidbody.mass);

            //跳跃请求计时时间内如果在地面且状态为非溶入 则进行跳跃操作
            if (Time.fixedTime - Model.LastJumpReqTime < playerSetting.JumpJudgeDuration && !Model.MeltStatus && IsGroundByCollider()) {
                Model.Jump = true;
            }
            if (Model.Jump) {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, playerSetting.JumpInitialSpeed);
                Model.Jump = false;
                rigidbody.drag = playerSetting.Air_LinearDrag; //跳跃的瞬间帧使用空中阻力
                Model.LastJumpReqTime = float.MinValue;
            }

        }

        /// <summary>根据输入、当前速度、最大速度确定有效输入(一维)</summary>
        private float GetValidInput(float input, float velocity, float maxMoveSpeed) {
            //如果同向
            if (Math.Sign(input) == Math.Sign(velocity)) {
                //如果当前速度据对值于最大速度 忽略输入
                if (Math.Abs(velocity) > maxMoveSpeed) {
                    return 0;
                }
                //否则返回正常系数的输入
                else return input;
            }
            //反向时返回增强系数的输入
            else return View.PlayerSetting.InvertDirectionMultiplier * input;
        }

        /// <summary>根据输入、当前速度、最大速度确定有效输入(二维)</summary>
        private Vector2 GetValidInput(Vector2 input, Vector2 velocity, Vector2 maxMoveSpeed) {
            return new Vector2(GetValidInput(input.x, velocity.x, maxMoveSpeed.x), GetValidInput(input.y, velocity.y, maxMoveSpeed.y));
        }

        /// <summary>当前和MeltArea相交且颜色对应正确</summary>
        private bool IsMeltAvaliable() {
            return Model.CurrentStayMeltArea != null && Model.CurrentStayMeltArea.ColorIndex == Model.CurrentColorIndex;
        }

        [Obsolete("IsGroundByCollider判断更精确")]
        private bool IsGround() {
            return Physics2D.Linecast(View.PlayerView.Player.transform.position, View.PlayerView.GroundChecker.position, 1 << LayerMask.NameToLayer("Ground"));
        }

        /// <summary>当前是否在地面</summary>
        private bool IsGroundByCollider() {
            return View.PlayerView.GroundCheckCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        }

        /// <summary>设置溶入状态</summary>
        private void SetMeltStatus(bool meltStatus) {
            Model.MeltStatus = meltStatus;
            Model.LastMeltReqTime = float.MinValue;
            View.PlayerView.NormalEntity.SetActive(!Model.MeltStatus);
            View.PlayerView.MeltedEntity.SetActive(Model.MeltStatus);
        }

    }
}