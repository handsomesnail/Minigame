﻿using System.IO;
using System.Text;

using Biz.Gaming;

using UnityEngine;

using ZCore;

namespace Biz.Storage {

    public class StorageController : Controller<GamingModel, GamingView> {

        private const string STORAGE_FILENAME = "/StoragePoint.json";

        private string GetStoragePointFilename () {
            return Application.dataPath + STORAGE_FILENAME;
        }

        // TODO(longfeilu) 存档读档实现

        public void OnSaveStorageCommand (SaveStorageCommand cmd) {
            string json = JsonUtility.ToJson (cmd.StoragePoint);
            Debug.Log ("Save Storage: " + json);
            Debug.Log ("Save Storage: " + cmd.StoragePoint.ToString ());
            using (FileStream fs = new FileStream (GetStoragePointFilename (), FileMode.Create, FileAccess.Write)) {
                byte [] bs = new UTF8Encoding ().GetBytes (json);
                fs.Write (bs, 0, bs.Length);
                Debug.Log ("Save Storage Success");
            }
        }

        public void OnLoadStorageCommand (LoadStorageCommand cmd) {
            Debug.Log ("Load Storage");
            using (FileStream fs = new FileStream (GetStoragePointFilename (), FileMode.Open, FileAccess.Read)) {
                UTF8Encoding encode = new UTF8Encoding ();
                byte [] bs = new byte [1024];
                fs.Read (bs, 0, bs.Length);
                string json = encode.GetString (bs);
                StoragePoint point = JsonUtility.FromJson<StoragePoint> (encode.GetString (bs));
                Debug.Log ("Load Storage Success: " + json);
                Debug.Log ("Load Storage Success: " + point.ToString ());
            }
        }
    }

}