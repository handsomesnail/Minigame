using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ZCore;

namespace Biz.Utils {

	/// <summary>简易Toast显示</summary>
	public class Toast : CallerBehaviour {

		private const string ToastViewPath = "Views/ToastView";

		public Text ContentText;

		void Start() {
			StartCoroutine(CoroutineExtension.Wait(new WaitForSeconds(0.5f), () => {
				float duration = 1.0f;
				//Ease ease = Ease.OutQuart;
				ContentText.DOFade(0, duration);
				ContentText.transform.DOLocalMoveY(15.0f, duration).SetRelative().OnComplete(() => {
					Destroy(this.gameObject);
				});
			}));
		}

		public static void Create(string content) {
			GameObject ToastPrefab = Resources.Load<GameObject>(ToastViewPath);
			GameObject toastGo = GameObject.Instantiate(ToastPrefab);
			Toast toast = toastGo.GetComponent<Toast>();
			toast.ContentText.text = content;
		}
	}
}