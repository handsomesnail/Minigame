using System;
using ZCore;

namespace Biz.Storage {

    [Serializable]
    public class StoragePoint {
        public int Chapter;
        public int Point;

        public override string ToString () {
            return String.Format ("Chapter:{0}, Point:{1}", Chapter, Point);
        }
        // other
    }

    public class StorageCommand : Command {
        public override Type GetController () {
            return typeof (StorageController);
        }
    }

    public class SaveStorageCommand : StorageCommand {
        public StoragePoint StoragePoint { get; private set; }
        public SaveStorageCommand (StoragePoint point) {
            this.StoragePoint = point;
        }

        public SaveStorageCommand (int chapter, int point) {
            StoragePoint tmp = new StoragePoint ();
            tmp.Chapter = chapter;
            tmp.Point = point;
            this.StoragePoint = tmp;
        }
    }


    public class LoadStorageCommand : StorageCommand {

    }
}