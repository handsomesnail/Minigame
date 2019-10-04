using System.Collections;
using System.Collections.Generic;
using Biz.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ZCore;

namespace Biz.Item {
	public class ItemTip : CallerBehaviour {
		public CanvasGroup Content;
		public Text TipText;
		public float FadeInDuration;
		public float FadeOutDuration;
		public float StayDuration;

		private void Start() {
			Content.alpha = 0;
			Content.DOFade(1, FadeInDuration);
			StartCoroutine(CoroutineExtension.Wait(new WaitForSeconds(FadeInDuration + StayDuration), () => {
				if (this) {
					Content.DOFade(0, FadeOutDuration).OnComplete(() => {
						Destroy(this.gameObject);
					});
				}
			}));
		}

	}

}