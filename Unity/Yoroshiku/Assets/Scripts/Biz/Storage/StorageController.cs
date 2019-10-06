using System.Collections.Generic;
using System.IO;
using System.Text;
using Biz.Gaming;
using Biz.Utils.IO;
using UnityEngine;
using ZCore;

namespace Biz.Storage {

    public class StorageController : Controller<GamingModel, GamingView> {

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
                Model.StoragePoint = new StoragePoint () {
                    Chapter = -1
                };
            }

            Model.StoragePoint.Chapter = Model.MapIndex;
            Model.StoragePoint.Postion = cmd.StoragePoint.Postion;


            Save ();
        }

        private void Save () {
            // 收集品收集后在经过过关点或存档点时进行存档
            Model.StoragePoint.Items = Post<Biz.Item.ListCollectedCommand, List<string>> (new Biz.Item.ListCollectedCommand ());

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
                    IOUtil.GetFullUrl ("/storage/save"),
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
                StoragePoint point = null;
                try {
                    if (File.Exists (GetStoragePointFilename ())) {
                        string json = File.ReadAllText (GetStoragePointFilename (), Encoding.UTF8);
                        if (!string.IsNullOrWhiteSpace (json)) {
                            point = JsonUtility.FromJson<StoragePoint> (json);
                            if (point.Items == null) point.Items = new List<string> ();
                            if (point.UnlockedChapters == null) {
                                point.UnlockedChapters = new List<int> {
                                    0
                                };
                            }
                        }
                    } else {
                        Debug.Log ("there is no local storage.");
                        point = new StoragePoint () {
                            Chapter = -1,
                            Items = new List<string> (),
                            UnlockedChapters = new List<int> () { 0 }
                        };
                    }
                    Model.StoragePoint = point;
                } catch {
                    Debug.Log ("failed to load storage from local.");
                }
                cmd.callback?.Invoke (point);
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
                IOUtil.GetFullUrl ("/storage/load"),
                form,
                (HttpResponse obj) => {
                    if (obj.code == 0 && obj.data != null && !string.IsNullOrWhiteSpace (obj.data.ToString ())) {
                        storagePoint = JsonUtility.FromJson<StoragePoint> (obj.data.ToString ());
                        // todo  call before enter chapter Call (new Biz.Item.InitCommand (storagePoint.Items));
                    } else {
                        storagePoint = new StoragePoint () {
                            Chapter = -1,
                            Items = new List<string> (),
                            UnlockedChapters = new List<int> () { 0 }
                        };
                    }
                    cmd.callback?.Invoke (storagePoint);
                    Model.StoragePoint = storagePoint;
                },
                (float obj) => {
                    // ignore
                })
            );
        }

        public void OnUnlockAllCommand (UnlockAllCommand cmd) {
            if (Model.StoragePoint == null) {
                Model.StoragePoint = new StoragePoint () {
                    Chapter = -1
                };
            }
            int [] all = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            Model.StoragePoint.UnlockedChapters = new List<int> (all);

            Save ();
        }

        public void OnLastPlayCommand (LastPlayCommand cmd) {
            if (Model.StoragePoint == null) {
                Model.StoragePoint = new StoragePoint () {
                    Chapter = -1
                };
            }
            if (Model.StoragePoint.UnlockedChapters == null) {
                Model.StoragePoint.UnlockedChapters = new List<int> () { 0 };
            }
            if (!Model.StoragePoint.UnlockedChapters.Exists ((obj) => obj == Model.MapIndex)) {
                Model.StoragePoint.UnlockedChapters.Add (Model.MapIndex);
            }
            Model.StoragePoint.LastPlayChapter = Model.MapIndex;

            Save ();
        }

        public void OnPassChapterCommand (PassChapterCommand cmd) {
            if (Model.StoragePoint == null) {
                Model.StoragePoint = new StoragePoint () {
                    Chapter = -1
                };
            }
            if (Model.StoragePoint.PassChapters == null) {
                Model.StoragePoint.PassChapters = new List<int> ();
            }
            if (!Model.StoragePoint.PassChapters.Exists ((obj) => obj == Model.MapIndex)) {
                Model.StoragePoint.PassChapters.Add (Model.MapIndex);
            }

            Save ();
        }
    }

}