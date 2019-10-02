using System;
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
        public string [] Items;
        public InitCommand(params string [] items) {
            Items = items ?? (new string [0]);
        }
    }
}
