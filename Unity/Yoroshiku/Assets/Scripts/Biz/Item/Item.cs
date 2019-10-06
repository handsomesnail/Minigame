using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using ZCore;

namespace Biz.Item {

    /// <summary>
    /// 收集品
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Item : CallerBehaviour {

        public string ItemName; //Key
        /// <summary>
        /// 收集时显示的文本
        /// </summary>
        public string Text;

        public float FadeInDuration;
        public float FadeOutDuration;

        /// <summary>
        /// 文本显示的时间，单位s
        /// </summary>
        public float StayDuration;

        public Vector2 TipPosition;

        /// <summary>
        /// 销毁时的已该节点为根进行销毁
        /// </summary>
        public GameObject ItemRoot;

        public GameObject ItemCollectEffect;

        public AudioSource Audio;

        public Color EffectColor;

        void Start() {
            // 查看当前存档中该收集品是否已收集
            List<string> collected = Post<ListCollectedCommand, List<string>>(new ListCollectedCommand());
            foreach (var item in collected) {
                if (ItemName == item) {
                    Destroy(ItemRoot);
                    return;
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.name == "MoveCheckerCollider") {
                Call(new CollectCommand(this));
                Destroy(ItemRoot);
            }
        }

    }
}