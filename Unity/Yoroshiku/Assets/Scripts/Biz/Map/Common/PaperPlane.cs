using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Player;
using UnityEngine;
using ZCore;

namespace Biz.Map {

    /// <summary>纸飞机</summary>
    public class PaperPlane : CallerBehaviour, IAttachable {

        //是否是溶入触发
        public bool MeltTriggered;
        private Vector3 currentPlanePos;
        //上一个fixedUpdate物理模拟的位移量
        private Vector2 currentOffset;

        private Animation planeAnim;

        public virtual Vector3 CurrentMoveOffset {
            get {
                return currentOffset;
            }
        }

        private void Awake() {
            planeAnim = GetComponent<Animation>();
            if (!MeltTriggered) {
                planeAnim.playAutomatically = true;
            }
        }

        public void Start() {
            currentPlanePos = transform.position;
        }

        public void FixedUpdate() {
            currentOffset = transform.position - currentPlanePos;
            currentPlanePos = transform.position;
        }

        public virtual void OnPlayerMove(Vector2 moveForce) { }

        public virtual void OnStartAttached() {
            if (MeltTriggered) {
                planeAnim.Play();
            }
        }

    }
}