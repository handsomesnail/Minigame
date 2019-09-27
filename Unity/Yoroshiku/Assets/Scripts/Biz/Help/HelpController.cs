using ZCore;
using Biz.Gaming;
using UnityEngine;
namespace Biz.Help {
    public class HelpController : Controller<HelpModel, HelpView> {
        public void OnHelpCommand(HelpCommand cmd) {
            View.CloseButton.onClick.AddListener (delegate {
                View.Destroy ();
                Call (new Biz.Start.StartCommand ());
            });
        }
    }
}
