using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Player {

    [Serializable]
    public class PlayerView {
        public Transform Player;

        public Rigidbody2D Rigidbody;

        public GameObject NormalEntity;

        public GameObject MeltedEntity;

        public Transform GroundChecker;

        public Collider2D GroundCheckCollider;
    }
}