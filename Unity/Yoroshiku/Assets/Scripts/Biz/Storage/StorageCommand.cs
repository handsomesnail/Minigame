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
            StoragePoint tmp = new StoragePoint {
                Chapter = chapter,
                Postion = position
            };
            this.StoragePoint = tmp;
        }
    }


    public class LoadStorageCommand : StorageCommand {

    }
}