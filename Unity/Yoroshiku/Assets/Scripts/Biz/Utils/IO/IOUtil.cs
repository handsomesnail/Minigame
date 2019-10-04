using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
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

        /// <summary>Get请求 </summary>
        public static IEnumerator Get(string url, Action<HttpResponse> completeCallback, Action<float> progressCallback = null) {
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return Request(request, completeCallback, progressCallback);
        }

        /// <summary>Post请求</summary>
        public static IEnumerator Post(string url, Dictionary<string, string> formFields, Action<HttpResponse> completeCallback, Action<float> progressCallback = null) {
            UnityWebRequest request = UnityWebRequest.Post(url, formFields);
            request.timeout = 2; //2s超时
            yield return Request(request, completeCallback, progressCallback);
        }

        private static IEnumerator Request(UnityWebRequest request, Action<HttpResponse> completeCallback, Action<float> progressCallback) {
            request.SendWebRequest();
            while (!request.isDone) {
                progressCallback?.Invoke(request.downloadProgress);
                yield return null;
            }
            progressCallback?.Invoke(1.0f);
            if (request.isHttpError || request.isNetworkError) {
                Dialog.Create("网络正在开小差");
            }
            else {
                string data = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                Debug.Log(data);
                HttpResponse response = JsonConvert.DeserializeObject<HttpResponse>(data);
                completeCallback.CheckedInvoke(response);
            }
        }
        /// <summary>获取完整请求路径，添加host </summary>
        public static string GetFullUrl(string path) {
#if UNITY_EDITOR
            return "http://49.235.98.96:8080" + path;
#else
            return "http://49.235.98.96:8080" + path;
#endif
        }

    }

}