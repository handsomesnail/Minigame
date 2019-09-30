using ZCore;
using UnityEngine;
using System;
using System.Collections;
using Biz.Utils.IO;
using System.Collections.Generic;
using Biz.Gaming;

namespace Biz.Account {
    public class AccountController : Controller<GamingModel, AccountView> {
        private const string BASE_URL = "http://localhost:8080";

        public void OnIndexCommand (IndexCommand cmd) {
            View.TipPanel.SetActive (false);
            SwitchFunc (View.IndexRoot);
            InitView ();
        }

        private void InitView () {
            View.ToRegister.onClick.AddListener (delegate {
                SwitchFunc (View.RegisterRoot);
            });

            View.ToLogin.onClick.AddListener (delegate {
                SwitchFunc (View.LoginRoot);
            });

            View.RegisterBack.onClick.AddListener (delegate {
                SwitchFunc (View.IndexRoot);
            });

            View.LoginBack.onClick.AddListener (delegate {
                SwitchFunc (View.IndexRoot);
            });

            View.RegisterButton.onClick.AddListener (delegate {
                if (string.IsNullOrWhiteSpace (View.RegisterUsername.text)) {

                }

                if (string.IsNullOrWhiteSpace (View.RegisterPassword.text)) {

                }

                if (View.RegisterRepeatPassword.text != View.RegisterPassword.text) {

                }

                Dictionary<string, string> form = new Dictionary<string, string> {
                    { "username", View.RegisterUsername.text },
                    { "password", View.RegisterPassword.text }
                };

                StartCoroutine (
                    IOUtil.Post (
                    BASE_URL + "/account/register",
                    form,
                    (HttpResponse obj) => {
                        if (obj.code != 0) {
                            ShowTip (obj.msg);
                            return;
                        }
                        Model.Token = obj.data.ToString ();
                        View.Destroy ();
                        Call (new Biz.Start.StartCommand ());
                        Call (new Biz.Storage.LoadStorageCommand ());
                    },
                    (float obj) => {
                        // ignore
                    })
                );
            });

            View.LoginButton.onClick.AddListener (delegate {
                if (string.IsNullOrWhiteSpace (View.LoginUsername.text)) {
                    return;
                }
                if (string.IsNullOrWhiteSpace (View.LoginPassword.text)) {
                    return;
                }

                Dictionary<string, string> form = new Dictionary<string, string> {
                    { "username", View.LoginUsername.text },
                    { "password", View.LoginPassword.text }
                };

                StartCoroutine (
                    IOUtil.Post (
                    BASE_URL + "/account/login",
                    form,
                    (HttpResponse obj) => {
                        if (obj.code != 0) {
                            ShowTip (obj.msg);
                            return;
                        }
                        Model.Token = obj.data.ToString ();
                        View.Destroy ();
                        Call (new Biz.Start.StartCommand ());
                        Call (new Biz.Storage.LoadStorageCommand ());
                    },
                    (float obj) => {
                        // ignore
                    })
                );
            });
        }

        private void SwitchFunc (GameObject root) {
            GameObject [] roots = { View.IndexRoot, View.RegisterRoot, View.LoginRoot };
            foreach (var r in roots) {
                r.SetActive (r.name == root.name);
            }
        }

        /// <summary>
        /// 显示错误提示，如用户名不存在，密码不匹配等。
        /// </summary>
        /// <param name="tip">Tip.</param>
        private void ShowTip (string tip) {
            StartCoroutine (ShowTip0 (tip));
        }

        private IEnumerator ShowTip0 (string tip) {
            Debug.Log (123456);

            View.Tip.text = tip;
            View.TipPanel.SetActive (true);
            yield return new WaitForSeconds (1);
            View.Tip.text = string.Empty;
            View.TipPanel.SetActive (false);
        }
    }
}
