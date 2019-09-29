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
            Model.MeltStatus = false;
            Model.AttachedObject = null;
            Model.Offset = Vector2.zero;
            Model.Jump = false;
            Model.CurrentStayMeltAreas = new List<MeltArea>();
            Model.LastExitMeltArea = null;
            Model.LastJumpReqTime = float.MinValue;
            Model.LastMeltReqTime = float.MinValue;
            Model.StayedGround = null;
            Model.LastMeltOutTime = float.MinValue;
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
            if (IsMeltAvaliable() && !Model.MeltStatus) {
                SetMeltStatus(true);
            }
            else if (!Model.MeltStatus) {
                Model.LastMeltReqTime = Time.fixedTime;
            }
        }

        public void OnEnterMeltAreaCommand(EnterMeltAreaCommand cmd) {
            Debug.Log("EnterMeltArea");
            Model.CurrentStayMeltAreas.Add(cmd.MeltArea);
        }

        public void OnExitMeltAreaCommand(ExitMeltAreaCommand cmd) {
            Debug.Log("ExitMeltArea");
            Model.CurrentStayMeltAreas.Remove(cmd.MeltArea);
            Model.LastExitMeltArea = cmd.MeltArea;
        }

        public void OnSpringPushForceCommand(SpringPushForceCommand cmd) {
            View.PlayerView.Rigidbody.AddForce(cmd.Force);
        }

        public PlayerData OnGetPlayerDataCommand(GetPlayerDataCommand cmd) {
            return new PlayerData() {
                MeltStatus = Model.MeltStatus,
            };
        }

        public void OnDeadAreaTriggerCommand(DeadAreaTriggerCommand cmd) {
            Call(new Biz.Player.InitCommand());
        }

        public void OnSetStayedGroundCommand(SetStayedGroundCommand cmd) {
            Model.StayedGround = cmd.Ground;
        }

        private void FixedUpdate() {
            if (Model.GameStatus == GameStatus.Gaming) {
                UpdatePlayer();
            }
        }

        private void UpdatePlayer() {
            PlayerSetting playerSetting = View.PlayerSetting;
            Rigidbody2D rigidbody = View.PlayerView.Rigidbody;
            Animator playerAnim = View.PlayerView.PlayerAnim;
            playerAnim.SetBool("IsMelted", Model.MeltStatus);
            playerAnim.SetBool("IsGround", IsGroundByCollider());

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
                    gravity = playerSetting.GroundGravity;
                    //moveForceDirection = new Vector2(GetValidInput(Model.Offset.x, rigidbody.velocity.x, maxMoveSpeed), 0);
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
                //如果有移动力则设置方向
                if (moveForceDirection.x != 0) {
                    Vector3 playerScale = View.PlayerView.Player.localScale;
                    View.PlayerView.Player.localScale = moveForceDirection.x > 0 ? new Vector3(Math.Abs(playerScale.x), playerScale.y, playerScale.z) : new Vector3(-1 * Math.Abs(playerScale.x), playerScale.y, playerScale.z);
                }
                //溶出过程中忽略碰撞体(感觉不行)
                // float meltOutProcess = Time.fixedTime - Model.LastMeltOutTime;
                // View.PlayerView.NormalMoveCheckCollider.enabled = meltOutProcess > playerSetting.MeltOutDuration;
            }
            //溶入状态
            else {
                moveForce = playerSetting.Melted_MoveForce;
                maxMoveSpeed = playerSetting.Melted_MaxMoveSpeed;
                linearDrag = playerSetting.Melted_LinearDrag;
                gravity = 0;
                moveForceDirection = GetValidInput(Model.Offset, rigidbody.velocity, new Vector2(maxMoveSpeed, maxMoveSpeed));
                //依附处理
                if (Model.AttachedObject != null) {
                    View.PlayerView.PlayerTransform.position = View.PlayerView.PlayerTransform.position + Model.AttachedObject.CurrentMoveOffset;
                    Model.AttachedObject.OnPlayerMove(moveForceDirection);
                }
                //溶入状态下如果在MeltInDuration后的一帧进行检查 若通过推力仍没有相交(可能受输入的影响) 自动溶出
                float meltProcess = Time.fixedTime - Model.LastMeltTime;
                //+-0.5帧长 保证仅一次判定
                if (Model.MeltStatus && meltProcess >= playerSetting.MeltInDuration - 0.5f * Time.fixedDeltaTime && meltProcess < playerSetting.MeltInDuration + 0.5f * Time.fixedDeltaTime) {
                    if (!CheckMeltStatus()) {
                        Debug.Log("溶入失败");
                        Model.CurrentStayMeltAreas.RemoveAt(Model.CurrentStayMeltAreas.Count - 1);
                        SetMeltStatus(false, false);
                    }
                    else {
                        Debug.Log("溶入成功");
                    }
                }
            }
            #endregion

            rigidbody.gravityScale = gravity / (-1 * Physics2D.gravity.y);
            rigidbody.drag = linearDrag * rigidbody.mass;
            rigidbody.AddForce(moveForceDirection * (moveForce + linearDrag) * rigidbody.mass);
            playerAnim.SetBool("IsMove", moveForceDirection != Vector2.zero);

            //跳跃请求计时时间内如果在地面且状态为非溶入 则进行跳跃操作
            if (Time.fixedTime - Model.LastJumpReqTime < playerSetting.JumpJudgeDuration && !Model.MeltStatus && IsGroundByCollider()) {
                Model.Jump = true;
            }
            if (Model.Jump) {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, playerSetting.JumpInitialSpeed);
                Model.Jump = false;
                rigidbody.drag = playerSetting.Air_LinearDrag; //跳跃的瞬间帧使用空中阻力和重力
                rigidbody.gravityScale = (playerSetting.Gravity + playerSetting.Air_LinearDrag) / (-1 * Physics2D.gravity.y);
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

        /// <summary>获取主角在地面行走的目标方向(人物中心到地面的垂线为法向量) </summary>
        // private Vector2 GetGroundValidInput(Vector2 ValidInput) {
        //     Vector2 normalDir = Physics2D.Distance(View.PlayerView.CenterCollider, Model.StayedGround).normal;
        // }

        /// <summary>当前和MeltArea相交(且颜色对应正确[已去掉])</summary>
        private bool IsMeltAvaliable() {
            return Model.CurrentStayMeltAreas.Count != 0;
        }

        [Obsolete("IsGroundByCollider判断更精确")]
        private bool IsGround() {
            return Physics2D.Linecast(View.PlayerView.Player.transform.position, View.PlayerView.GroundChecker.position, 1 << LayerMask.NameToLayer("Ground"));
        }

        /// <summary>当前是否在地面</summary>
        private bool IsGroundByCollider() {
            return View.PlayerView.GroundCheckCollider.IsTouchingLayers(LayerMask.GetMask("AlwaysBarrier")) ||
                View.PlayerView.GroundCheckCollider.IsTouchingLayers(LayerMask.GetMask("Barrier"));
        }

        //只有在溶入过程结束后还没进入区域的情况不push
        /// <summary>设置溶入状态</summary>
        private void SetMeltStatus(bool meltStatus, bool push = true) {
            Model.MeltStatus = meltStatus;
            Model.LastMeltReqTime = float.MinValue;
            View.PlayerView.NormalEntity.SetActive(!Model.MeltStatus);
            View.PlayerView.MeltedEntity.SetActive(Model.MeltStatus);
            if (meltStatus) {
                Model.LastMeltTime = Time.fixedTime;
            }
            else {
                Model.LastMeltOutTime = Time.fixedTime;
            }
            //依附处理
            if (meltStatus) {
                foreach (MeltArea meltArea in Model.CurrentStayMeltAreas) {
                    IAttachable attachedObject = meltArea.GetComponentInParent<IAttachable>();
                    if (attachedObject != null) {
                        Model.AttachedObject = attachedObject;
                    }
                }
            }
            else {
                Model.AttachedObject = null;
            }
            //溶入和溶出时的推力处理
            if (push) {
                if (meltStatus) {
                    //已经相交则不需要推力进入溶入区域
                    if (!CheckMeltStatus()) {
                        ColliderDistance2D distance2D = Physics2D.Distance(Model.CurrentStayMeltAreas[0].GetComponent<Collider2D>(), View.PlayerView.CenterCollider);
                        Vector2 v0 = GetPushInVelocity(distance2D.normal * distance2D.distance, View.PlayerSetting.MeltInDuration);
                        View.PlayerView.Rigidbody.velocity = View.PlayerSetting.MeltInPushMultiplier * v0; //1.8
                    }
                }
                else {
                    ColliderDistance2D distance2D = Physics2D.Distance(View.PlayerView.CenterCollider, Model.LastExitMeltArea.GetComponent<Collider2D>());
                    Vector2 pushForce = GetPushOutForce(distance2D.normal);
                    View.PlayerView.Rigidbody.AddForce(View.PlayerSetting.MeltOutPushMultiplier * pushForce);
                }
            }
        }

        /// <summary>获取溶入初速度</summary>
        private Vector2 GetPushInVelocity(Vector2 distance, float duration) {
            return distance / duration;
        }

        /// <summary>获取溶出推力/summary>
        private Vector2 GetPushOutForce(Vector2 direction) {
            Vector2 pushForce = direction * 500;
            Debug.Log("推力：" + pushForce);
            return pushForce;
        }

        /// <summary>检查溶入状态(在溶入过程开始后一段时间检查推力是否使其溶入成功) </summary>
        private bool CheckMeltStatus() {
            bool status = false;
            Collider2D meltCollider = Model.CurrentStayMeltAreas[0].GetComponent<Collider2D>();
            if (View.PlayerView.UnMeltCheckCollider.IsTouching(meltCollider)) {
                Debug.Log("刚好相交");
                status = true;
            }
            else if (meltCollider.OverlapPoint(View.PlayerView.Rigidbody.position)) {
                Debug.Log("进入内部");
                status = true;
            }
            else {
                status = false;
            }
            Debug.Log("检查溶入状态:" + status);
            return status;
        }

    }
}