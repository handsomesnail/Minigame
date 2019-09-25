using System;
using ZCore;
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

        public SaveStorageCommand (int chapter, int point, params int[] items) {
            StoragePoint tmp = new StoragePoint {
                Chapter = chapter,
                Point = point,
                Items = new String [items.Length]
            };
            items.CopyTo (tmp.Items, 0);
            this.StoragePoint = tmp;
        }
    }


    public class LoadStorageCommand : StorageCommand {

    }
}