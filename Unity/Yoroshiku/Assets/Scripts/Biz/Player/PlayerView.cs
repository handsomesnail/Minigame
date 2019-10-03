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

        public SpriteRenderer PlayerRenderer;

        //用来计算溶入助力的碰撞体
        public CircleCollider2D CenterCollider;

        public Animator PlayerAnim;

        public Rigidbody2D Rigidbody;

        public AudioSource PlayerAudio;

        public GameObject NormalEntity;

        public GameObject MeltedEntity;

        public GameObject MeltedMoveEffect;

        public Transform GroundChecker;

        public Collider2D GroundCheckCollider;

        public Collider2D MeltCheckCollider;

        public Collider2D UnMeltCheckCollider;

        public Collider2D NormalMoveCheckCollider;

        public Collider2D GroundCenterCollider;

        [Header("Resource Reference")]
        public GameObject MeltInEffect;
        public GameObject MeltOutEffect;

    }
}