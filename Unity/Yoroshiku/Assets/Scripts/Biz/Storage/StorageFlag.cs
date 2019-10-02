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

        private void OnCollisionEnter2D (Collision2D collision) {
            Debug.Log (23);
        }

        private void OnTriggerEnter2D (Collider2D collision) {
            Debug.Log (11);
            if ((DateTime.Now - LastInvoked).CompareTo (Interval) < 0) return;
            LastInvoked = DateTime.Now;
            Call (new SaveStorageCommand (new StoragePoint (transform.position)));
        }

    }
}