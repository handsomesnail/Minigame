using System;
using UnityEngine;

namespace Biz.Storage {
    [Serializable]
    public class StoragePoint {
        public int Chapter;
        public int PassChapter;
        public Vector3 Postion;
        public string[] Items;

        public StoragePoint() { }

        public StoragePoint(Vector3 position) {
            Postion = position;
        }

        public StoragePoint(int chapter, Vector3 postion) {
            Chapter = chapter;
            Postion = postion;
        }

        public StoragePoint(int passChapter) {
            PassChapter = passChapter;
        }

        public override string ToString() {
            return string.Format("Chapter:{0}, Postion:{1}, Items:{2}", Chapter, Postion.ToString(), Items.ToString());
        }
    }
}