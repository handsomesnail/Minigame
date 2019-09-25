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

        public Animator PlayerAnim;

        public Rigidbody2D Rigidbody;

        public GameObject NormalEntity;

        public GameObject MeltedEntity;

        public Transform GroundChecker;

        public Collider2D GroundCheckCollider;
    }
}