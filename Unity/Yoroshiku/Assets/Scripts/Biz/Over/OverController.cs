using Biz.Gaming;
using Biz.Loading;
using Biz.Storage;
using Biz.Utils;
using DG.Tweening;
using UnityEngine;
using ZCore;

namespace Biz.Over {
    public class OverController : Controller<GamingModel, OverView> {
        public void OnShowOverViewCommand(ShowOverViewCommand cmd) {
            OverView view = View;
            View.OverImage.transform.DOLocalMoveY(2000.0f, 40.0f).OnComplete(Return).SetEase(Ease.Linear);
        }

        private void Return() {
            Call(new TransitCommand(() => {
                View.Destroy();
                Call(new Start.StartCommand());
            }, false));
        }

    }
}