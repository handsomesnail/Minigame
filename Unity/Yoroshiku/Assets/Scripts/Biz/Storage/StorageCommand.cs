using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Storage {

    public class StorageCommand : Command {
        public override Type GetController() {
            return typeof(StorageController);
        }
    }

    public class SaveStorageCommand : StorageCommand {
        public StoragePoint StoragePoint { get; private set; }
        public SaveStorageCommand(StoragePoint point) {
            this.StoragePoint = point;
        }

        public SaveStorageCommand(int chapter, Vector3 position) {
            this.StoragePoint = new StoragePoint {
                Chapter = chapter,
                Postion = position
            };
        }
    }

    public class LoadStorageCommand : StorageCommand {
        public Action<StoragePoint> callback;

        public LoadStorageCommand() { }

        public LoadStorageCommand(Action<StoragePoint> callback) {
            this.callback = callback;
        }
    }

    /// <summary>删除本地存档文件</summary>
    public class DeleteLocalStorageCommand : StorageCommand {

    }

    public class UnlockAllCommand : StorageCommand {

    }

    public class PassChapterCommand : StorageCommand {

    }

    public class LastPlayCommand : StorageCommand {

    }
}