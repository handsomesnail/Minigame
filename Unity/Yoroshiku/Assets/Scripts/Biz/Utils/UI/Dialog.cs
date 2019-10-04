using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ZCore;

namespace Biz.Utils {

	/// <summary>简易Dialog显示</summary>
	public class Dialog : CallerBehaviour {

		private const string DialogViewPath = "Views/DialogView";

		public Text ContentText;

		public Transform DialogRoot;

		void Start() {
			DialogRoot.localScale = new Vector3(0.6f, 0.6f, 0.6f);
			DialogRoot.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutElastic);
		}

		public static void Create(string content) {
			GameObject DialogPrefab = Resources.Load<GameObject>(DialogViewPath);
			GameObject dialogGo = GameObject.Instantiate(DialogPrefab);
			Dialog dialog = dialogGo.GetComponent<Dialog>();
			dialog.ContentText.text = content;
		}

		public void Close() {
			Destroy(this.gameObject);
		}

	}
}