using UnityEngine;
using ZCore;
using UnityEngine.UI;
using System.Collections;
using System.Threading;

namespace Biz.Item {


    /// <summary>
    /// 收集品
    /// </summary>
    [RequireComponent (typeof (Collider2D))]
    public class Item : CallerBehaviour {

        private volatile bool TextInactive = false;

        private volatile bool TriggerInactive = false;

        public string ItemName;
        /// <summary>
        /// 收集时显示的文本
        /// </summary>
        public string Text;

        /// <summary>
        /// 文本显示的时间，单位ms
        /// </summary>
        public uint Duration;

        /// <summary>
        /// 暂停页显示的Sprite, 未设置则显示关卡内Sprite
        /// </summary>
        public Sprite Sprite;

        /// <summary>
        /// 销毁时的已该节点为根进行销毁
        /// </summary>
        public GameObject ItemRoot;

        public GameObject CanvasRoot;

        public Text TextDisplayer;

        void Start () {
            // 查看当前存档中该收集品是否已收集
            string [] collected = Post<ListCollectedCommand, string []> (new ListCollectedCommand ());
            foreach (var item in collected) {
                if(ItemName == item) {
                    Destroy (this);
                    return;
                }
            }

            TextDisplayer.text = Text;
            Duration = Duration == 0 ? 3000 : Duration;
            CanvasRoot.SetActive (false);
        }

        private void OnTriggerEnter2D (Collider2D collision) {
            if (!collision.isTrigger) {
                Debug.Log ("OnTriggerEnter2D");
                Call (new CollectCommand (this));
                StartCoroutine (DisplayText ());
                StartCoroutine (PlayAnimation ());
            }
        }

        private IEnumerator DisplayText () {
            CanvasRoot.SetActive (true);
            yield return new WaitForSeconds (Duration / 1000.0f);
            CanvasRoot.SetActive (false);
            if (TriggerInactive) {
                Destroy (ItemRoot);
            } else {
                TextInactive = true;
            }
        }

        private IEnumerator PlayAnimation () {
            Animation ani = GetComponent<Animation> ();
            ani.Play ();
            yield return new WaitForSeconds (ani.clip == null ? 0 : ani.clip.length);

            GetComponent<SpriteRenderer> ().sprite = null;
            if (TextInactive) {
                Destroy (ItemRoot);
            } else {
                TriggerInactive = true;
            }
        }

    }
}
