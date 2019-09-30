using Biz.Gaming;

using UnityEngine;
using Biz.Storage;
using ZCore;
namespace Biz.Start {
    public class StartController : Controller<StartModel, StartView> {

        public void OnStartCommand (StartCommand cmd) {
            View.StartButton.onClick.AddListener (delegate {
                View.Destroy ();
                Call (new EnterCommand ());
            });

            View.HelpButton.onClick.AddListener (delegate {
                View.Destroy ();
                Call (new Biz.Help.HelpCommand ());
            });

            View.ExitBUtton.onClick.AddListener (delegate {
                //View.Destroy ();
                //Call(new Biz.Pause.PauseCommand());
                //Application.Quit ();
                View.Destroy ();
                Call (new Biz.Account.IndexCommand ());
            });

        }
    }
}