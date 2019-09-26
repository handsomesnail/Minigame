using UnityEngine;
using ZCore;
using UnityEngine.UI;
using System.Collections;
namespace Biz.Item {


    /// <summary>
    /// 收集品
    /// </summary>
    [RequireComponent (typeof (Collider2D))]
    public class Item : CallerBehaviour {

        public static GameObject ItemUI;

        /// <summary>
        /// 收集时显示的文本
        /// </summary>
        public string Text;

        /// <summary>
        /// 文本显示的时间，单位ms
        /// </summary>
        public uint Duration;

        /// <summary>
        /// 收集时播放的动画
        /// </summary>
        public Animation Animation;

        /// <summary>
        /// 收集时播放的音频
        /// </summary>
        public AudioSource Audio;

        void Start () {
            Debug.Log ("enter Start");
            // TODO
            // 查看当前存档中该收集品是否已收集
        }

        private void OnCollisionEnter2D (Collision2D collision) {
            Debug.Log ("OnCollisionEnter2D");
            if (Animation != null) {
                Animation.Play ();
            }
            if (Audio != null) {
                Audio.Play ();
            }
            InstantiateItemUI ();
            StartCoroutine (AutoDestroy());
        }

        private void OnTriggerEnter2D (Collider2D collision) {
            Debug.Log ("OnCollisionEnter2D");
            if (Animation != null) {
                Animation.Play ();
            }
            if (Audio != null) {
                Audio.Play ();
            }
            InstantiateItemUI ();
            StartCoroutine (AutoDestroy ());
        }

        private IEnumerator AutoDestroy () {
            float d = Duration == 0 ? 3 : Duration / 1000.0f;
            yield return new WaitForSeconds (d);
            Destroy (gameObject);
            DestroyItemUI ();
        }

        private void InstantiateItemUI() {
            GameObject prefab = Resources.Load<GameObject> ("UI/Item");
            if(prefab == null) {
                Debug.LogError ("Prefab UI/Item is null");
                return;
            }
            ItemUI = Instantiate (prefab);
            
        }

        private void DestroyItemUI () {
            Destroy (ItemUI);
            ItemUI = null;
        }

    }
}
