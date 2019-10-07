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
        public bool IsReturn;
        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.name == "MoveCheckerCollider") {
                Call(new TransitCommand(() => {
                    if (!IsReturn) {
                        Pass();
                    }
                    else {
                        Return();
                    }
                }, true));
            }
        }

        private void Pass() {
            Call(new PassChapterCommand()); // 记录通关
            Call(new ExitCommand());
            Call(new EnterCommand(MapIndex));
        }

        private void Return() {
            Call(new PassChapterCommand()); // 记录通关
            Call(new ExitCommand());
            Call(new Over.ShowOverViewCommand());
        }
    }
}