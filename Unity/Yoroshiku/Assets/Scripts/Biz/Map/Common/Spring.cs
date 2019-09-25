using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Gaming;
using Biz.Player;
using UnityEngine;
using ZCore;

namespace Biz.Map {

    /// <summary>弹簧</summary>
    public class Spring : CallerBehaviour {

        /// <summary>弹簧的劲度系数</summary>
        public float K;

        /// <summary>主角受到的弹力系数(弹力可以固定，也可以和弹簧伸缩量成正比)</summary>
        public float Bounciness;

        /// <summary>接收碰撞的平台刚体</summary>
        public Rigidbody2D Platform;

        public Collider2D PlatformCollider;

        /// <summary>上面平台竖直方向运动</summary>
        public Transform Top;

        /// <summary>底部固定不动</summary>
        public Transform Bottom;

        private Vector2 currentVelocity;

        /// <summary>弹簧当前长度</summary>
        public float Length {
            get { return Top.position.y - Bottom.position.y; }
        }

        /// <summary>弹簧伸长量</summary>
        public float X {
            get { return StaticLength - Length; }
        }

        /// <summary>弹力(F=kx)</summary>
        public float F {
            get {
                return K * X;
            }
        }

        /// <summary>弹簧的静止长度</summary>
        private float StaticLength = 0;
        private void Awake() {
            StaticLength = Length;
        }

        private void FixedUpdate() {
            if (Platform.velocity.y >= 0 && currentVelocity.y < 0 && PlatformCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) {
                Call(new SpringPushForceCommand(new Vector2(0, Bounciness)));
            }
            currentVelocity = Platform.velocity;
            if (Math.Abs(X) > 0.01f) {
                Platform.AddForce(new Vector2(0, F));
            }
        }

    }
}