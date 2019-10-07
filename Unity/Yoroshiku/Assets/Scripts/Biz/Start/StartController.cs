using Biz.Gaming;
using Biz.Storage;
using Biz.Utils;
using UnityEngine;
using ZCore;
namespace Biz.Start {
    public class StartController : Controller<GamingModel, StartView> {

        public void OnStartCommand(StartCommand cmd) {
            View.StartButton.onClick.AddListener(delegate {
                View.Destroy();
                Call(new Biz.Chapter.ChapterCommand());
                //Call (new Biz.Item.InitCommand (Model.StoragePoint?.Items));
                //Call (new EnterCommand ());
            });

            View.HelpButton.onClick.AddListener(delegate {
                View.Destroy();
                Call(new Biz.Help.HelpCommand());
            });

            View.ExitBUtton.onClick.AddListener(delegate {
                //View.Destroy ();
                //Call(new Biz.Pause.PauseCommand());
                //Application.Quit ();
                View.Destroy();
                Call(new Biz.Account.IndexCommand());
            });

            View.ClearButton.onClick.AddListener(() => {
                Call(new DeleteLocalStorageCommand());
                Call(new Biz.Storage.LoadStorageCommand());
                Dialog.Create("清除本地缓存成功");
            });

        }
    }
}