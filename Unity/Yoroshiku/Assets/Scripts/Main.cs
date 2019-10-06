using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Biz.Account;
using Biz.Map;
using Biz.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using ZCore;

/// <summary>逻辑入口脚本</summary>
public class Main : CallerBehaviour {

    //[Debug]如果通过ToIndex脚本过来，自动加载ToIndex脚本所在场景的Map
    public static string DebugMapName = string.Empty;

    private void Start() {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = -1;
        EventSystem.current.pixelDragThreshold = Screen.height / 50;
        StartGame();
    }

    private void StartGame() {
        //总内容不多的时候尽量PreLoad所有Map到内存
        if (!string.IsNullOrEmpty(DebugMapName)) {
            Call(new Biz.Gaming.EnterCommand());
        }
        else {
            //显示Splash图片
            //加载所有资源
            //进入选关界面
            Call(new IndexCommand());
        }
    }

    [ContextMenu("Test")]
    public void Test() {
        //StartGame();
        Debug.Log("加载MapList");
        MapList maplist = Resources.Load<MapList>("Configs/MapList");
        GameObject.Instantiate(maplist.Maps[1].gameObject);
    }

}