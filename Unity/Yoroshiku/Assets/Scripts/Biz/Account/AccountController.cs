using ZCore;
using UnityEngine;
using System;
using System.Collections;

namespace Biz.Account {
    public class AccountController : Controller<AccountModel, AccountView> {
        public void OnIndexCommand(IndexCommand cmd) {
            View.TipPanel.SetActive (false);
            SwitchFunc (View.IndexRoot);
            InitView ();
        }

        private void InitView() {
            View.ToRegister.onClick.AddListener (delegate {
                SwitchFunc (View.RegisterRoot);
            });

            View.ToLogin.onClick.AddListener (delegate {
                SwitchFunc (View.LoginRoot);
            });

            View.RegisterButton.onClick.AddListener (delegate {
                //todo
            });

            View.LoginButton.onClick.AddListener (delegate {
                ShowTip ("sadad");
                //todo
            });
        }

        private void SwitchFunc(GameObject root) {
            Debug.Log ("a");
            GameObject [] roots = { View.IndexRoot, View.RegisterRoot, View.LoginRoot };
            foreach(var r in roots) {
                r.SetActive (r.name == root.name);
            }
        }

        /// <summary>
        /// 显示错误提示，如用户名不存在，密码不匹配等。
        /// </summary>
        /// <param name="tip">Tip.</param>
        private void ShowTip(string tip) {
            StartCoroutine (ShowTip0 (tip));
        }

        private IEnumerator ShowTip0(string tip) {
            Debug.Log (123456);

            View.Tip.text = tip;
            View.TipPanel.SetActive (true);
            yield return new WaitForSeconds (1);
            View.Tip.text = string.Empty;
            View.TipPanel.SetActive (false);
        }
    }
}
