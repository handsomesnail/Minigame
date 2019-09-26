using ZCore;
using Biz.Gaming;
using UnityEngine;
namespace Biz.Help {
    public class HelpController : Controller<GamingModel, GamingView> {

        private static GameObject HelpUI;

        private void Start () {
            if(gameObject.name == "HelpUI") {
                HelpUI = gameObject;
                HelpUI.SetActive (false);
            }
            //HelpUI = GameObject.Find("HelpUI");
        }

        public void OnHelpCommand(HelpCommand cmd) {
            Debug.Log (HelpUI.name);
            HelpUI.SetActive (true);
        }

        public void OnCloseClick() {
            HelpUI.SetActive (false);
            Call (new Biz.Start.IndexCommand ());
        }
    }
}
