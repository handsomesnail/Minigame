using System.IO;
using System.Text;
using Biz.Gaming;
using UnityEngine;
using ZCore;

namespace Biz.Storage {

    public class StorageController : Controller<GamingModel, GamingView> {

        private const string STORAGE_FILENAME = "/StoragePoint.json";

        private string GetStoragePointFilename() {
#if UNITY_EDITOR
            return Application.dataPath + STORAGE_FILENAME;
#else
            return Application.persistentDataPath + STORAGE_FILENAME;
#endif
        }

        public void OnSaveStorageCommand(SaveStorageCommand cmd) {
            // List collected items
            string[] items = Post<Biz.Item.ListCollectedCommand, string[]>(new Biz.Item.ListCollectedCommand());
            cmd.StoragePoint.Items = items;
            cmd.StoragePoint.Chapter = Model.MapIndex;

            string json = JsonUtility.ToJson(cmd.StoragePoint);
            Debug.Log("Save Storage: " + json);
            using(FileStream fs = new FileStream(GetStoragePointFilename(), FileMode.Create, FileAccess.Write)) {
                byte[] bs = new UTF8Encoding().GetBytes(json);
                fs.Write(bs, 0, bs.Length);
                Debug.Log("Save Storage Success");
            }
        }

        /// <summary>
        /// On load storage command. Check return value.
        /// </summary>
        /// <returns>StoragePoint.</returns>
        /// <param name="cmd">Cmd.</param>
        public StoragePoint OnLoadStorageCommand(LoadStorageCommand cmd) {
            Debug.Log("Load Storage");
            using(FileStream fs = new FileStream(GetStoragePointFilename(), FileMode.Open, FileAccess.Read)) {
                UTF8Encoding encode = new UTF8Encoding();
                byte[] bs = new byte[1024];
                fs.Read(bs, 0, bs.Length);
                string json = encode.GetString(bs);
                StoragePoint point = JsonUtility.FromJson<StoragePoint>(encode.GetString(bs));
                Debug.Log("Load Storage Success: " + point.ToString());
                if (point.Items == null) point.Items = new string[0];
                return point;
            }
        }
    }

}