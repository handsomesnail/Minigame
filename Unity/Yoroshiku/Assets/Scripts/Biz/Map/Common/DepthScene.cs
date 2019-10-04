using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Map {
    public class DepthScene : CallerBehaviour {
        //暂不需要指定
        private Camera targetCamera;
        //越远值越小
        public float XMoveSpeed;
        public float YMoveSpeed;

        private Vector3 targetPos;

        private void Awake() {
            targetCamera = Camera.main;
        }
        private void Start() {
            targetPos = targetCamera.transform.position;
        }

        private void Update() {
            Vector3 offset = targetCamera.transform.position - targetPos;
            float x = transform.position.x + XMoveSpeed * offset.x;
            float y = transform.position.y + YMoveSpeed * offset.y;
            transform.position = new Vector3(x, y, transform.position.z);
            targetPos = targetCamera.transform.position;
        }

    }
}