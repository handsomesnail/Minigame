using ZCore;
using Biz.Gaming;
using UnityEngine;
using UnityEngine.UI;
using Biz.Item;
using Biz.Storage;
namespace Biz.Pause {
    public class PauseController : Controller<GamingModel, PauseView> {

        public void OnPauseCommand (PauseCommand cmd) {
            View.ContinueButton.onClick.AddListener (delegate {
                View.Destroy ();
                Call (new Biz.Gaming.ResumeCommand ());
            });

            View.RestartButton.onClick.AddListener (delegate {
                View.Destroy ();
                Call (new Biz.Gaming.ExitCommand ());
                Call (new Biz.Item.InitCommand (Model.StoragePoint?.Items));
                Call (new Biz.Gaming.EnterCommand (Model.MapIndex));
            });

            View.HomeButton.onClick.AddListener (delegate {
                View.Destroy ();
                Call (new Biz.Gaming.ExitCommand ());
                Call (new Biz.Start.StartCommand ());
            });
        }
    }

}
