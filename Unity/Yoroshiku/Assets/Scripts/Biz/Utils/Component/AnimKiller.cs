using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

public class AnimKiller : CallerBehaviour {
    private Animator anim;
    public string StateName;
    void Start() {
        anim = this.GetComponent<Animator>();
    }

    void Update() {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName(StateName) && info.normalizedTime >= 1.0f) {
            Destroy(this.gameObject);
        }
    }
}