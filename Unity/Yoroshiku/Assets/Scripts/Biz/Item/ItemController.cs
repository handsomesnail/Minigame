using UnityEngine;
using System.Collections;
using ZCore;
using Biz.Gaming;
using Biz.Storage;
using System.Collections.Generic;

namespace Biz.Item {
    public class ItemController : Controller<ItemModel, ItemView> {

        public void OnInitCommand (InitCommand cmd) {
            Model.Items = new List<string> (cmd.Items);
        }

        public void OnCollectCommand (CollectCommand cmd) {
            //Call (new Biz.Gaming.PauseCommand ());
            //View.ItemText.text = cmd.Item.Text;
            foreach ( string s in Model.Items) {
                if (s == cmd.Item.ItemName) return;
            }
            Model.Items.Add (cmd.Item.ItemName);

            //StartCoroutine (AutoDestroy (cmd));
        }

        public string [] OnListCollectedCommand (ListCollectedCommand cmd) {
            return Model.Items.ToArray ();
        }

        private IEnumerator AutoDestroy (CollectCommand cmd) {
            yield return new WaitForSeconds (cmd.Item.Duration == 0 ? 3 : cmd.Item.Duration / 1000.0f);
            View.Destroy ();
            Call (new Biz.Gaming.ResumeCommand ());
        }
    }
}