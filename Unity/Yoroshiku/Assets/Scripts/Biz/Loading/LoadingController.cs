using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using ZCore;

namespace Biz.Loading {
    public sealed class LoadingController : Controller<LoadingModel, LoadingView> {
        public void OnTransitCommand(TransitCommand cmd) {
            StartCoroutine(Transit(cmd));
        }

        private IEnumerator Transit(TransitCommand cmd) {
            yield return null;
            View.Mask.DOFade(1, 0.5f);
            yield return new WaitForSeconds(1.0f);
            cmd.TransitionCallback.CheckedInvoke();
            if (cmd.TransitionAsyncCallback != null) {
                yield return StartCoroutine(cmd.TransitionAsyncCallback);
            }
            yield return null;
            if (cmd.ClearResource) {
                Resources.UnloadUnusedAssets();
                GC.Collect();
                Debug.Log("****清理内存****");
            }
            yield return null;
            View.Mask.DOFade(0, 0.5f);
            yield return new WaitForSeconds(0.5f);
            View.Destroy();
        }
    }
}