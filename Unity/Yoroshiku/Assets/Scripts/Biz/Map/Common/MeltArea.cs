using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Player;
using UnityEngine;
using ZCore;

namespace Biz.Map {

    [RequireComponent(typeof(Collider2D))]
    public class MeltArea : CallerBehaviour {

        public Color SplatterColor = Color.white;

        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.name == "MeltCheckerCollider") {
                Call(new EnterMeltAreaCommand(this));
            }
        }
        private void OnTriggerStay2D(Collider2D collider) {

        }
        private void OnTriggerExit2D(Collider2D collider) {
            if (collider.gameObject.name == "MeltCheckerCollider") {
                Call(new ExitMeltAreaCommand(this));
            }
            if (collider.gameObject.name == "UnMeltCheckerCollider") {
                Call(new MeltOutCommand(this));
            }
        }
    }
}