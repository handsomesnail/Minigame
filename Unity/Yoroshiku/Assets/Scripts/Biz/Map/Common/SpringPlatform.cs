using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Map {

    /// <summary>弹簧平台</summary>
    public class SpringPlatform : CallerBehaviour {

        public bool IsGround = false;

        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.name == "GroundCheckerCollider") {
                Debug.Log("触碰Ground");
                IsGround = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collider) {
            if (collider.gameObject.name == "GroundCheckerCollider") {
                Debug.Log("离开Ground");
                IsGround = false;
            }
        }

    }
}