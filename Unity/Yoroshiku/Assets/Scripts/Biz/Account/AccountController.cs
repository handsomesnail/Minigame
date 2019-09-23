using Biz.Gaming;
using Biz.Storage;

using UnityEngine;

using ZCore;

namespace Biz.Account {
    public class AccountController : Controller<GamingModel, GamingView> {

        void OnIndexCommand (IndexCommand cmd) {
            Show ();
        }

        public void OnExitClick () {
            Debug.Log ("OnExitClick");
            Call (new LoadStorageCommand ());
        }

        public void OnStartClick () {
            Debug.Log ("OnStartClick");
            Call (new SaveStorageCommand (1, 1));
            //			Hide ();
            //			Call (new EnterCommand ());
        }

        void Hide () {
            CanvasGroup canvas = GetComponent<CanvasGroup> ();
            canvas.alpha = 0;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;

        }

        void Show () {
            CanvasGroup canvas = GetComponent<CanvasGroup> ();
            canvas.alpha = 1;
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
        }
    }
}