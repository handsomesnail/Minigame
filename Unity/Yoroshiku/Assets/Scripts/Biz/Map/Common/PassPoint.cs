using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Gaming;
using UnityEngine;
using ZCore;

namespace Biz.Map {

    [RequireComponent(typeof(Collider2D))]
    public class PassPoint : CallerBehaviour {
        public int MapIndex;
        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.name == "MoveCheckerCollider") {
                Call(new ExitCommand());
                Call(new EnterCommand(MapIndex));
            }
        }
    }
}