using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Biz.Account;
using Biz.Map;
using Biz.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ZCore;

/// <summary>逻辑入口脚本</summary>
public class Main : CallerBehaviour {

    //[Debug]如果通过ToIndex脚本过来，自动加载ToIndex脚本所在场景的Map
    public static string DebugMapName = string.Empty;

    public GameObject SplashCanvas;
    public Image Splash1;
    public Image Splash2;

    private void Start() {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = -1;
        EventSystem.current.pixelDragThreshold = Screen.height / 50;
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame() {
        //总内容不多的时候尽量PreLoad所有Map到内存
        if (!string.IsNullOrEmpty(DebugMapName)) {
            yield return null;
            Destroy(SplashCanvas);
            Call(new Biz.Gaming.EnterCommand());
        }
        else {
            yield return new WaitForSeconds(1.0f);
            GameObject GamingView = Resources.Load<GameObject>("Views/GamingView");
            Splash1.DOFade(0, 1.0f);
            Splash2.DOFade(1, 1.0f).OnComplete(() => {
                StartCoroutine(CoroutineExtension.Wait(new WaitForSeconds(1.0f), () => {
                    Destroy(SplashCanvas);
                    Call(new IndexCommand());
                }));
            });
            //Call(new IndexCommand());
        }
    }

    [ContextMenu("Test")]
    public void Test() { }

}