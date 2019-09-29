using System;
using System.Collections;
using UnityEngine;
using ZCore;

namespace Biz.Storage {

    [RequireComponent(typeof(Collider2D))]
    public class StorageFlag : CallerBehaviour {
        private bool Invoked;

        //public int Chapter;

        // Use this for initialization
        void Start() {
            Debug.Log("Start");
        }

        // Update is called once per frame
        void Update() { }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (Invoked) return;
            Invoked = true;
            Call(new Biz.Storage.SaveStorageCommand(new StoragePoint(transform.position)));
            Debug.Log("OnTriggerEnter2D");
            Debug.Log(DateTime.Now.Millisecond);
        }

    }
}