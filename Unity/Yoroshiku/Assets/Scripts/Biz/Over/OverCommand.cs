using System;
using ZCore;

namespace Biz.Over {
    public class OverCommand : Command {
        public override Type GetController() {
            return typeof(OverController);
        }
    }

    public class ShowOverViewCommand : OverCommand {

    }

}