using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Player;
using UnityEngine;
using ZCore;

namespace Biz.Map {
    /// <summary>钟摆类物体</summary>
    public class Pendulum : MonoBehaviour, IAttachable {

        /// <summary>移动附加力乘数</summary>
        public float MoveForceMultiplier;
        public Rigidbody2D FixPoint;
        public Rigidbody2D Shaker;
        public DistanceJoint2D Joint2D;

        public Transform RopeTransform;
        public Transform RopoEnd;

        private Vector3 currentShakerPos;

        //上一个fixedUpdate物理模拟的位移量
        private Vector2 currentOffset;

        public virtual Vector3 CurrentMoveOffset {
            get {
                return currentOffset;
            }
        }

        public virtual bool IsLock() {
            return false;
        }

        public void Start() {
            currentShakerPos = Shaker.transform.position;
        }

        public void FixedUpdate() {
            currentOffset = Shaker.transform.position - currentShakerPos;
            currentShakerPos = Shaker.transform.position;
            double angle = Math.Atan((RopoEnd.position.x - FixPoint.position.x) / (RopoEnd.position.y - FixPoint.position.y)) * 180 / Math.PI;
            RopeTransform.eulerAngles = new Vector3(0, 0, -(float) angle);
        }

        public virtual void OnPlayerMove(Vector2 moveForce) {
            Shaker.AddForce(moveForce * MoveForceMultiplier);
        }

        public virtual void OnStartAttached(Rigidbody2D playerRigidbod) { }

    }
}