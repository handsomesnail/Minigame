using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Biz.Map;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZCore;

public class ToIndex : CallerBehaviour {

    private void Awake() {
        BaseMap _map = GameObject.FindObjectOfType<BaseMap>();
        if (_map != null) {
            Main.DebugMapName = _map.gameObject.name;
        }
        SceneManager.LoadScene("Index");
    }

}