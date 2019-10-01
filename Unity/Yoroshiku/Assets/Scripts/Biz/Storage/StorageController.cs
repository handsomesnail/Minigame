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

        private const string STORAGE_FILENAME = "/StoragePoint.json";

        private string GetStoragePointFilename () {
#if UNITY_EDITOR
            return Application.dataPath + STORAGE_FILENAME;
#else
            return Application.persistentDataPath + STORAGE_FILENAME;
#endif
        }
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

            if (Model.IsGuest) {
                Debug.Log ("save storage to local.");
                string json = JsonUtility.ToJson (Model.StoragePoint);
                try {
                    File.WriteAllText (GetStoragePointFilename (), json, Encoding.UTF8);
                } catch {
                    Debug.Log ("failed to save storage to local.");
                }
                return;
            }

            if (string.IsNullOrWhiteSpace (Model.Token)) {
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
                        Debug.Log ("failed to save storage to server.");
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
            if (Model.IsGuest) {
                Debug.Log ("load storage from local.");
                try {
                    StoragePoint point = null;
                    if (File.Exists (GetStoragePointFilename ())) {
                        string json = File.ReadAllText (GetStoragePointFilename (), Encoding.UTF8);
                        if (!string.IsNullOrWhiteSpace (json)) {
                            point = JsonUtility.FromJson<StoragePoint> (json);
                            if (point.Items == null) point.Items = new string [0];
                        }
                    } else {
                        Debug.Log ("there is no local storage.");
                    }
                    Model.StoragePoint = point;
                } catch {
                    Debug.Log ("failed to load storage from local.");
                }
                return;
            }

            if (string.IsNullOrWhiteSpace (Model.Token)) {
                return;
            }

            Debug.Log ("load storage form server.");
            StoragePoint storagePoint = null;
            Dictionary<string, string> form = new Dictionary<string, string> {
                { "token", Model.Token }
            };
            StartCoroutine (
                IOUtil.Post (
                BASE_URL + "/storage/load",
                form,
                (HttpResponse obj) => {
                    if (obj.code != 0) {
                        Debug.Log ("failed to load storage from server.");
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