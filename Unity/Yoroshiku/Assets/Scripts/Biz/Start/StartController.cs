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

            View.ContinueButton.onClick.AddListener (delegate {
                View.Destroy ();
                Call (new EnterCommand ());
                StoragePoint storage = Post<LoadStorageCommand, StoragePoint> (new LoadStorageCommand());
                Call (new EnterCommand (storage.Chapter));

            });

            View.HelpButton.onClick.AddListener (delegate {
                View.Destroy ();
                Call (new Biz.Help.HelpCommand ());
            });

            View.ExitBUtton.onClick.AddListener (delegate {
                //View.Destroy ();
                //Call(new Biz.Pause.PauseCommand());
                Application.Quit ();
            });

        }
    }
}