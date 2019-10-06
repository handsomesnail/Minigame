﻿using System;
using System.Collections.Generic;
using ZCore;

namespace Biz.Item {
    public class ItemCommand : Command {
        public override Type GetController () {
            return typeof (ItemController);
        }
    }

    public class CollectCommand : ItemCommand {
        public Item Item { get; }

        public CollectCommand (Item item) {
            this.Item = item;
        }
    }

    public class ListCollectedCommand : ItemCommand {

    }

    public class InitCommand : ItemCommand {
        public List<string> Items;
        public InitCommand (List<string> items) {
            Items = new List<string> (items ?? new List<string>());
        }
    }
}
