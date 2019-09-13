using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZCore;

public class ToIndex : CallerBehaviour {

    private void Awake() {
        SceneManager.LoadScene("Index");
    }

}