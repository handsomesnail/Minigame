using System;
using System.Collections;
using UnityEngine;
using ZCore;

namespace Biz.Storage {

    [RequireComponent (typeof (Collider2D))]
    public class StorageFlag : CallerBehaviour {
        private static readonly TimeSpan Interval = new TimeSpan (0, 0, 2);
        private DateTime LastInvoked = DateTime.MinValue;
        //public int Chapter;

        // Use this for initialization
        void Start () {
        }

        // Update is called once per frame
        void Update () { }

        private void OnCollisionExit2D (Collision2D collision) {
        }

        private void OnTriggerEnter2D (Collider2D collision) {
            if ((DateTime.Now - LastInvoked).CompareTo (Interval) < 0) return;
            LastInvoked = DateTime.Now;
            Call (new SaveStorageCommand (new StoragePoint (transform.position)));
            Debug.Log ("OnTriggerEnter2D");
            Debug.Log (DateTime.Now.Millisecond);
        }

    }
}