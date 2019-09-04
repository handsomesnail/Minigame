using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Biz.Utils.IO {

    public static class IOUtil {

        public static void Write(string folderPath, string fileName, byte[] content) {
            string fullPath = folderPath + "/" + fileName;
            if (!Directory.Exists(folderPath)) {
                Directory.CreateDirectory(folderPath);
            }
            using(FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write)) {
                fs.Write(content, 0, content.Length);
            }
        }

        private static IEnumerator ReadAsync(string fullPath, Action<byte[]> completeCallback) {
#if UNITY_ANDROID
            UnityWebRequest req = UnityWebRequest.Get(fullPath);
            yield return req.SendWebRequest();
            completeCallback.CheckedInvoke(req.downloadHandler.data);
#else
            using(FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read)) {
                using(StreamReader sw = new StreamReader(fs)) {
                    byte[] bytes = new byte[fs.Length];
                    Task<int> task = fs.ReadAsync(bytes, 0, bytes.Length);
                    yield return task.WaitCompleted();
                    completeCallback.CheckedInvoke(bytes);
                }
            }
#endif
        }

        public static void WriteSerializableObject(string folderPath, string fileName, object graph) {
            string fullPath = folderPath + "/" + fileName;
            if (!Directory.Exists(folderPath)) {
                Directory.CreateDirectory(folderPath);
            }
            using(FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write)) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, graph);
            }
        }

        public static object ReadSerializableObject(string folderPath, string fileName) {
            string fullPath = folderPath + "/" + fileName;
            using(FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read)) {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(fs);
            }
        }

        public static IEnumerator GetFileMD5HashAsync(string filePath, Action<string> completeCallback) {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            int len = (int) fs.Length;
            byte[] data = new byte[len];
            Task<int> task = fs.ReadAsync(data, 0, len);
            yield return task.WaitCompleted();
            fs.Close();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            string fileMD5 = "";
            foreach (byte b in result) {
                fileMD5 += Convert.ToString(b, 16);
            }
            completeCallback.CheckedInvoke(fileMD5);
        }

    }

}