using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Player;
using UnityEngine;
using ZCore;

namespace Biz.Map {

    /// <summary>喷气区域</summary>
    [RequireComponent(typeof(Collider2D))]
    public class JetArea : CallerBehaviour {

        public float ForceMultiplier;
        public Transform JetSource;

        //public AnimationCurve Curve;

        private void OnTriggerEnter2D(Collider2D collider) { }

        private void OnTriggerStay2D(Collider2D collider) {
            if (collider.gameObject.name == "MoveCheckerCollider") {
                Call(new JetedCommand(this));
            }
        }

        private void OnTriggerExit2D(Collider2D collider) { }
    }
}