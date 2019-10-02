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

        private Collider2D _Collider;

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
            _Collider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collider2D) {
            //Barrier或者AlwaysBarrier层 且Collider为非Trigger才能被视为Ground
            if (collider2D.gameObject.name == "GroundCheckerCollider" && _Collider.isTrigger == false) {
                Call(new SetStayedGroundCommand(_Collider));
            }
        }

        private void OnTriggerStay2D(Collider2D collider2D) {
            if (collider2D.gameObject.name == "GroundCheckerCollider" && _Collider.isTrigger == false) {
                Call(new SetStayedGroundCommand(_Collider));
            }
        }

        private void OnTriggerExit2D(Collider2D collider2D) {
            if (collider2D.gameObject.name == "GroundCheckerCollider" && _Collider.isTrigger == false) {
                Call(new SetStayedGroundCommand(null));
            }
        }

    }

}