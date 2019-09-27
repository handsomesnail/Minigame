using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Player;
using UnityEngine;
using ZCore;

namespace Biz.Map {

    //引入BarrierType作为PlanB
    public enum BarrierType {
        Always = 0, //任何状态下生效
        NormalStatus, //只在普通状态下生效, 勾选这个主角在普通状态下无法通过
        MeltedStatus, //只在溶入状态下生效, 勾选这个主角在溶入状态下无法通过
    }

    [RequireComponent(typeof(Collider2D))]
    public class BarrierArea : CallerBehaviour {

        public BarrierType Type;

        private void Start() {
            if (Type == BarrierType.Always) {
                gameObject.layer = LayerMask.NameToLayer("AlwaysBarrier");
            }
            else if (Type == BarrierType.MeltedStatus) {
                gameObject.layer = LayerMask.NameToLayer("MeltBarrier");
            }
            else if (Type == BarrierType.NormalStatus) {
                gameObject.layer = LayerMask.NameToLayer("Barrier");
            }
        }

        // private void OnCollisionEnter2D(Collision2D collision2D) {
        //     if (collision2D.collider.gameObject.name == "MoveCheckerCollider") {
        //         PlayerData playerData = Post<GetPlayerDataCommand, PlayerData>(new GetPlayerDataCommand());
        //         if (!CheckCollision(playerData.MeltStatus)) {
        //             Debug.Log("忽略碰撞");
        //             GetComponent<Collider2D>().isTrigger = true;
        //         }
        //     }
        // }

        // private void OnTriggerEnter2D(Collider2D collider2D) {
        //     if (collider2D.gameObject.name == "MoveCheckerCollider") {
        //         PlayerData playerData = Post<GetPlayerDataCommand, PlayerData>(new GetPlayerDataCommand());
        //         if (CheckCollision(playerData.MeltStatus)) {
        //             Debug.Log("设置碰撞");
        //             GetComponent<Collider2D>().isTrigger = false;
        //         }
        //     }
        // }

        // /// <summary>检查碰撞条件</summary>
        // private bool CheckCollision(bool meltStatus) {
        //     //正常状态碰到溶入障碍 或 溶入状态碰到正常障碍地面 忽略碰撞
        //     return !((!meltStatus && Type == BarrierType.MeltedStatus) ||
        //         (meltStatus && Type == BarrierType.NormalStatus));
        // }

    }

}