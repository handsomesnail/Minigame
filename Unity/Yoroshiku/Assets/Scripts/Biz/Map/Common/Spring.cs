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
        private float currentLength;

        /// <summary>Player是否踩在弹簧上</summary>
        public SpringPlatform SpringPlatform;

        // /// <summary>Player本次接触是否已受到推力</summary>
        // private bool IsCurrentTouchPush = false;

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

        //readyPush期间只能push一次
        private bool readyPush = false;

        private void Awake() {
            StaticLength = Length;
            currentLength = Length;
        }

        private void FixedUpdate() {
            if (SpringPlatform.IsGround && !readyPush) {
                //if (Platform.velocity.y >= 0 && currentVelocity.y < 0 && SpringPlatform.IsGround) {
                //if (Length >= StaticLength && Length < StaticLength && SpringPlatform.IsGround) {
                Debug.Log("准备推一次");
                readyPush = true;
                StartCoroutine(Push());
            }
            currentVelocity = Platform.velocity;
            currentLength = Length;
            if (Math.Abs(X) > 0.01f) {
                Platform.AddForce(new Vector2(0, F));
            }
        }

        private IEnumerator Push() {
            yield return new WaitForSeconds(0.2f);
            Call(new SpringPushForceCommand(new Vector2(0, Bounciness)));
            yield return new WaitForSeconds(0.2f);
            readyPush = false;
            Debug.Log("推");
        }

    }
}