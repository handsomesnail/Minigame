using Biz.Gaming;

using UnityEngine;

using ZCore;
namespace Biz.Account {
    public class AccountController : Controller<GamingModel, GamingView> {

        private static GameObject StartUI;

        public void OnIndexCommand (IndexCommand cmd) {
            if (StartUI == null) {
                GameObject Prefab = Resources.Load<GameObject> ("UI/Start");
                if (Prefab == null) {
                    Debug.LogError ("Prefab StartUI cannot be null");
                    return;
                }
                StartUI = Instantiate (Prefab, new Vector3 (0, 0, 0), Quaternion.identity);
            }
            StartUI.SetActive (true);
            //startUI. GetComponent<Canvas> ().enabled = true;
        }

        public void OnStartClick () {
            Debug.Log ("OnStartClick");
            StartUI.SetActive (false);
            //Call (new SaveStorageCommand (1, 1));
            //GetComponent<Canvas> ().enabled = false;

            Call (new EnterCommand ());
        }

        public void OnContinueClick () {
            GetComponent<Canvas> ().enabled = false;
        }

        public void OnHelpClick () {
            //SetActive (false);
            GameObject Prefab = Resources.Load<GameObject> ("UI/CollectItem");
            if (Prefab == null) {
                Debug.LogError ("Prefab CollectItem cannot be null");
                return;
            }
            GameObject @object = Instantiate (Prefab, new Vector3 (0, 0, 0), Quaternion.identity);
        }

        public void OnExitClick () {
            Debug.Log ("OnExitClick");
            //Call (new Chapter.SelectChapterCommand ());
            Application.Quit ();
            //Call (new LoadStorageCommand ());
        }
    }
}