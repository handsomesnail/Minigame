using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Player {

    [Serializable]
    public class PlayerView {

        public GameObject View;

        public Transform PlayerTransform;

        public Transform Player;

        //用来计算溶入助力的碰撞体
        public CircleCollider2D CenterCollider;

        public Animator PlayerAnim;

        public Rigidbody2D Rigidbody;

        public GameObject NormalEntity;

        public GameObject MeltedEntity;

        public Transform GroundChecker;

        public Collider2D GroundCheckCollider;

        public Collider2D MeltCheckCollider;

        public Collider2D UnMeltCheckCollider;

    }
}