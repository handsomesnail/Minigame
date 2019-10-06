using System;
using UnityEngine;
using System.Collections.Generic;
namespace Biz.Storage {
    [Serializable]
    public class StoragePoint {
        public int Chapter;
        public Vector3 Postion;
        public int LastPlayChapter;
        public List<string> Items;
        public List<int> UnlockedChapters;
        public List<int> PassChapters;

        public StoragePoint() { }

        public StoragePoint(Vector3 position) {
            Postion = position;
        }

        public StoragePoint(int chapter, Vector3 postion) {
            Chapter = chapter;
            Postion = postion;
        }

        public override string ToString() {
            return string.Format("Chapter:{0}, Postion:{1}, Items:{2}", Chapter, Postion.ToString(), Items.ToString());
        }
    }
}