using Biz.Gaming;

using UnityEngine;

using ZCore;
namespace Biz.Start {
    public class StartController : Controller<GamingModel, GamingView> {

        private static GameObject StartUI;

        private void Start () {
            if (gameObject.name == "StartUI") {
                StartUI = gameObject;
                StartUI.SetActive (false);
            }
        }

        public void OnIndexCommand (IndexCommand cmd) {
            //if (StartUI == null) {
            //    GameObject Prefab = Resources.Load<GameObject> ("UI/Start");
            //    if (Prefab == null) {
            //        Debug.LogError ("Prefab StartUI cannot be null");
            //        return;
            //    }
            //    StartUI = Instantiate (Prefab, new Vector3 (0, 0, 0), Quaternion.identity);
            //}
            StartUI.SetActive (true);
        }

        public void OnStartClick () {
            StartUI.SetActive (false);
            Call (new EnterCommand ());
        }

        public void OnContinueClick () {
            GetComponent<Canvas> ().enabled = false;
        }

        public void OnHelpClick () {
            StartUI.SetActive (false);
            Call (new Biz.Help.HelpCommand ());
            //SetActive (false);
            //GameObject Prefab = Resources.Load<GameObject> ("UI/CollectItem");
            //if (Prefab == null) {
            //    Debug.LogError ("Prefab CollectItem cannot be null");
            //    return;
            //}
            //GameObject @object = Instantiate (Prefab, new Vector3 (0, 0, 0), Quaternion.identity);
        }

        public void OnExitClick () {
            StartUI.SetActive (false);
            Call (new Biz.Pause.PauseCommand ());
            // Application.Quit ();
        }
    }
}