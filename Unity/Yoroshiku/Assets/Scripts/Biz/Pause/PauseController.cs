using ZCore;
using Biz.Gaming;
using UnityEngine;
using UnityEngine.UI;
using Biz.Item;
using Biz.Storage;
namespace Biz.Pause {
    public class PauseController : Controller<PauseModel, PauseView> {

        public void OnPauseCommand (PauseCommand cmd) {
            Debug.Log ("OnPauseCommand");
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

            StoragePoint storage = Post<LoadStorageCommand, StoragePoint> (new LoadStorageCommand ());
            Debug.Log (storage.ToString ());
            // Get Items Container
            GridLayoutGroup ItemContainer = View.ItemContainer;
            CollectItemList list = View.ItemList;
            Debug.Log (list.ToString ());
            list.Items.ForEach ((obj) => Debug.Log (obj.name));


            // First, Remove All 
            foreach (Transform tran in ItemContainer.transform) {
                Destroy (tran.gameObject);
            }

            // Load Prefab
            GameObject prefab = Resources.Load<GameObject> ("UI/ClickableItem");

            // Create Collected Items
            string [] names = storage.Items;
            names = new string [] { "CollectItem", "CollectItem" };
            foreach (string name_ in names) {
                // Find Source Item
                Item.Item src = list.Items.Find ((Item.Item obj) => name_ == obj.name);

                // Create Clickable Item
                GameObject i = Instantiate (prefab, ItemContainer.transform);
                i.transform.localScale = new Vector3 (1, 1, 1);
                i.transform.localPosition = new Vector3 (0, 0, 0);
                Sprite sprite = src.Sprite ?? src.gameObject.GetComponent<SpriteRenderer> ().sprite;
                i.GetComponent<Button> ().GetComponent<Image> ().sprite = sprite;
                i.GetComponent<Button> ().onClick.AddListener (delegate {
                    View.ItemText.text = src.Text;
                });
            }
        }
    }

}
