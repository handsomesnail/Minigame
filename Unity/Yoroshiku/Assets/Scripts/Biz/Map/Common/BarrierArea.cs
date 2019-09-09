using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Player;
using UnityEngine;
using ZCore;

namespace Biz.Map {

    //引入BarrierType作为PlanB
    public enum BarrierType {
        Default = 0, //任何状态下生效
        Normal, //只在普通状态下生效, 勾选这个主角在普通状态下无法通过
        Melted, //只在溶入状态下生效, 勾选这个主角在溶入状态下无法通过
    }

    [RequireComponent(typeof(Collider2D))]
    public class BarrierArea : CallerBehaviour {

        public BarrierType Type;

        private void Start() {
            //普通障碍物同时为Ground可以作为跳跃的平台
            if (Type == BarrierType.Normal || Type == BarrierType.Default) {
                gameObject.layer = LayerMask.NameToLayer("Ground");
            }
            //溶入状态下的障碍物只作为普通地图元素
            else if (Type == BarrierType.Melted) {
                gameObject.layer = LayerMask.NameToLayer("Map");
            }
        }

        private void OnCollisionEnter2D(Collision2D collision2D) {
            if (collision2D.collider.gameObject.name == "MoveCheckerCollider") {
                PlayerData playerData = Post<GetPlayerDataCommand, PlayerData>(new GetPlayerDataCommand());
                if (!CheckCollision(playerData.MeltStatus)) {
                    GetComponent<Collider2D>().isTrigger = true;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collider2D) {
            if (collider2D.gameObject.name == "MoveCheckerCollider") {
                PlayerData playerData = Post<GetPlayerDataCommand, PlayerData>(new GetPlayerDataCommand());
                if (CheckCollision(playerData.MeltStatus)) {
                    GetComponent<Collider2D>().isTrigger = false;
                }
            }
        }

        /// <summary>检查碰撞条件</summary>
        private bool CheckCollision(bool meltStatus) {
            //正常状态碰到溶入障碍 或 溶入状态碰到正常障碍地面 忽略碰撞
            return !((!meltStatus && Type == BarrierType.Melted) ||
                (meltStatus && Type == BarrierType.Normal));
        }

    }

}