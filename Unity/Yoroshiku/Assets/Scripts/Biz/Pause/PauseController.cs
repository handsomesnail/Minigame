using ZCore;
using Biz.Gaming;
using UnityEngine;
using UnityEngine.UI;
using Biz.Item;
using Biz.Storage;
namespace Biz.Pause {
    public class PauseController : Controller<GamingModel, GamingView> {
        private static GameObject PauseUI;

        public CollectItemList CollectItemList;

        private void Start () {
            if (gameObject.name == "PauseUI") {
                PauseUI = gameObject;
                PauseUI.SetActive (true);
            }
        }

        private void Awake () {
            if (gameObject.name != "PauseUI") return;

            StoragePoint storage = Post< LoadStorageCommand, StoragePoint> (new LoadStorageCommand());
            Debug.Log (storage.ToString ());
            // Get Items Container
            GameObject ItemContainer = GameObject.Find ("PauseUI/PauseCanvas/Items");
            CollectItemList list = View.CollectItemList;
            Debug.Log (list.ToString());
            list.Items.ForEach ((obj) => Debug.Log (obj.name));


            // First, Remove All 
            foreach (Transform tran in ItemContainer.transform) {
                Destroy (tran.gameObject);
            }

            // Load Prefab
            GameObject prefab = Resources.Load<GameObject> ("UI/ClickableItem");

            // Create Collected Items
            string [] names = storage.Items;
            foreach (string name_ in names) {
                // Find Source Item
                Item.Item src = list.Items.Find ((Item.Item obj) => name_ == obj.name);

                // Create Clickable Item
                GameObject i = Instantiate (prefab, ItemContainer.transform);
                i.transform.localScale = new Vector3 (1, 1, 1);
                i.transform.localPosition = new Vector3 (0, 0, 0);
                i.GetComponent<Button> ().GetComponent<Image> ().sprite = src.gameObject.GetComponent<SpriteRenderer> ().sprite;
                i.GetComponent<Button> ().onClick.AddListener (delegate {
                    Text text = GameObject.Find ("PauseUI/PauseCanvas/ItemText").GetComponent<Text> ();
                    text.text = src.Text;
                });
            }
        }

        public void OnPauseCommand (PauseCommand cmd) {
            PauseUI.SetActive (true);
        }

        public void OnContinueClick () {
            PauseUI.SetActive (false);
            // TODO
        }

        public void OnRestartClick () {
            PauseUI.SetActive (false);
            Call (new Biz.Gaming.EnterCommand ());
            // TODO
        }

        public void OnHomeClick () {
            PauseUI.SetActive (false);
            Call (new Biz.Start.IndexCommand ());
        }

        public void OnItemClick () {
            Debug.Log ("OnItemClick");
            Text text = GameObject.Find ("PauseUI/PauseCanvas/ItemText").GetComponent<Text> ();
            text.text = "12414";
        }
    }

}
