using System;
using ZCore;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Biz.Storage {

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

        public SaveStorageCommand (int chapter, Vector3 position) {
            this.StoragePoint = new StoragePoint {
                Chapter = chapter,
                Postion = position
            };
        }

        public SaveStorageCommand (int passChapter) {
            this.StoragePoint = new StoragePoint {
                PassChapter = passChapter
            };
        }
    }


    public class LoadStorageCommand : StorageCommand {
        public Action<StoragePoint> callback;

        public LoadStorageCommand () {
        }

        public LoadStorageCommand (Action<StoragePoint> callback) {
            this.callback = callback;
        }
    }
}