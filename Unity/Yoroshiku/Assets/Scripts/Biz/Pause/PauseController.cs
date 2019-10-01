using ZCore;
using Biz.Gaming;
using UnityEngine;
using UnityEngine.UI;
using Biz.Item;
using Biz.Storage;
namespace Biz.Pause {
    public class PauseController : Controller<PauseModel, PauseView> {

        public void OnPauseCommand (PauseCommand cmd) {
            View.ContinueButton.onClick.AddListener (delegate {
                View.Destroy ();
                Call (new Biz.Gaming.ResumeCommand ());
            });

            View.RestartButton.onClick.AddListener (delegate {
                View.Destroy ();
                Call (new Biz.Gaming.ExitCommand ());
                Call (new Biz.Gaming.EnterCommand ());
            });

            View.HomeButton.onClick.AddListener (delegate {
                View.Destroy ();
                Call (new Biz.Gaming.ExitCommand ());
                Call (new Biz.Start.StartCommand ());
            });
        }
    }

}
