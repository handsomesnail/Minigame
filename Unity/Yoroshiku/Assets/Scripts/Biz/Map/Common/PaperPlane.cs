using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Player;
using UnityEngine;
using ZCore;

namespace Biz.Map {

    /// <summary>纸飞机</summary>
    public class PaperPlane : CallerBehaviour, IAttachable {

        private Vector3 currentPlanePos;
        //上一个fixedUpdate物理模拟的位移量
        private Vector2 currentOffset;

        private Animator planeAnim;

        #region 飞机特殊处理
        [HideInInspector]
        public GameObject VirtualPlayer;
        private Vector3 currentVPPos;
        [HideInInspector]
        private Vector2 currentVPOffset;
        private bool IsStartFly;
        public Vector3 CurrentVPOffset {
            get {
                return currentVPOffset;
            }
        }
        #endregion

        public virtual Vector3 CurrentMoveOffset {
            get {
                return currentOffset;
            }
        }

        public virtual bool IsLock() {
            return true;
        }

        private void Awake() {
            planeAnim = GetComponent<Animator>();
        }

        public void Start() {
            VirtualPlayer = new GameObject("VirtualPlayer");
            VirtualPlayer.transform.SetParent(this.transform);
            currentPlanePos = transform.position;
            IsStartFly = false;
        }

        public void FixedUpdate() {
            currentOffset = transform.position - currentPlanePos;
            currentPlanePos = transform.position;
            if (IsStartFly) {
                currentVPOffset = VirtualPlayer.transform.position - currentVPPos;
                currentVPPos = VirtualPlayer.transform.position;
            }
        }

        public virtual void OnPlayerMove(Vector2 moveForce) { }

        public virtual void OnStartAttached(Rigidbody2D playerRigidbody) {
            StartCoroutine(CoroutineExtension.Wait(new WaitForSeconds(0.25f), () => {
                VirtualPlayer.transform.position = playerRigidbody.position;
                currentVPPos = VirtualPlayer.transform.position;
                planeAnim.Play("fly");
                IsStartFly = true;
            }));
        }

    }
}