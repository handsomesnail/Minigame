using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Biz.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using ZCore;

/// <summary>逻辑入口脚本</summary>
public class Main : CallerBehaviour {

    private void Start() {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = -1;
        EventSystem.current.pixelDragThreshold = Screen.height / 50;
        StartGame();
    }

    private void StartGame() {
        //总内容不多的时候尽量PreLoad所有Map到内存
        Call(new Biz.Gaming.ShowCommand(3)); //直接加载Map0启动游戏
    }

    [ContextMenu("Test")]
    public void Test() {
        StartGame();
    }

}