using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Gaming;
using Biz.Loading;
using Biz.Storage;
using UnityEngine;
using ZCore;

namespace Biz.Map {

    [RequireComponent(typeof(Collider2D))]
    public class PassPoint : CallerBehaviour {
        public int MapIndex;
        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.name == "MoveCheckerCollider") {
                Call(new TransitCommand(() => {
                    Pass();
                }, true));
            }
        }

        private void Pass() {
            Call(new SaveStorageCommand(MapIndex));
            Call(new ExitCommand());
            Call(new EnterCommand(MapIndex));
        }
    }
}