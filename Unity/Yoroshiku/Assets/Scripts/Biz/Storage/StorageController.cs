using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Biz.Gaming;
using Biz.Utils.IO;
using UnityEngine;
using ZCore;

namespace Biz.Storage {

    public class StorageController : Controller<GamingModel, GamingView> {
    
        private const string BASE_URL = "http://localhost:8080";

        public void OnSaveStorageCommand (SaveStorageCommand cmd) {
            if (Model.StoragePoint == null) {
                Model.StoragePoint = new StoragePoint ();
            }
            // 修改本地存档
            if (cmd.StoragePoint.PassChapter > Model.StoragePoint.PassChapter) {
                Model.StoragePoint.PassChapter = cmd.StoragePoint.PassChapter;
            } else {
                Model.StoragePoint.Chapter = Model.MapIndex;
                Model.StoragePoint.Postion = cmd.StoragePoint.Postion;
            }
            // 收集品收集后在经过过关点或存档点时进行存档
            Model.StoragePoint.Items = Post<Biz.Item.ListCollectedCommand, string []> (new Biz.Item.ListCollectedCommand ());

            if(string.IsNullOrWhiteSpace(Model.Token)) {
                return;
            }

            Debug.Log ("save storage to server.");
            Dictionary<string, string> form = new Dictionary<string, string> {
                { "token", Model.Token },
                { "storage", JsonUtility.ToJson (Model.StoragePoint) }
            };
            StartCoroutine (
                IOUtil.Post (
                BASE_URL + "/storage/save",
                form,
                (HttpResponse obj) => {
                    if (obj.code != 0) {
                        return;
                    }
                },
                (float obj) => {
                    // ignore
                })
            );
        }

        /// <summary>
        /// On load storage command. Check return value.
        /// </summary>
        /// <returns>StoragePoint.</returns>
        /// <param name="cmd">Cmd.</param>
        public void OnLoadStorageCommand (LoadStorageCommand cmd) {
            Debug.Log ("load storage form server.");
            StoragePoint storagePoint = null;
            Dictionary<string, string> form = new Dictionary<string, string> {
                    { "token", Model.Token },
                };
            StartCoroutine (
                IOUtil.Post (
                BASE_URL + "/storage/load",
                form,
                (HttpResponse obj) => {
                    if (obj.code != 0) {
                        return;
                    }
                    if (obj.data != null && !string.IsNullOrWhiteSpace (obj.data.ToString ())) {
                        storagePoint = JsonUtility.FromJson<StoragePoint> (obj.data.ToString ());
                        // todo  call before enter chapter Call (new Biz.Item.InitCommand (storagePoint.Items));
                    }
                    cmd.callback?.Invoke (storagePoint);
                    Model.StoragePoint = storagePoint;
                },
                (float obj) => {
                    // ignore
                })
            );
        }
    }

}