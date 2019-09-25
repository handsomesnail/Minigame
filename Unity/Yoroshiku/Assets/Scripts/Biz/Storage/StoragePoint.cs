using System;

namespace Biz.Storage {
    [Serializable]
    public class StoragePoint {
        public int Chapter;
        public int Point;
        public int [] Items;

        public override string ToString () {
            return string.Format ("Chapter:{0}, Point:{1}, Items:{2}", Chapter, Point, Items);
        }
    }
}