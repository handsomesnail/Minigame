using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Player;
using UnityEngine;
using ZCore;

namespace Biz.Map {
    public class MeltArea : CallerBehaviour {
        public int ColorIndex;

        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.CompareTag("Player")) {
                Call(new EnterMeltAreaCommand(this));
            }
        }
        private void OnTriggerStay2D(Collider2D collider) {

        }
        private void OnTriggerExit2D(Collider2D collider) {
            if (collider.CompareTag("Player")) {
                Call(new ExitMeltAreaCommand(this));
            }
        }
    }
}