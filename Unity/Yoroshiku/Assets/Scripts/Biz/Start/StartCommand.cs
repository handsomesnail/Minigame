using System;
using ZCore;

namespace Biz.Start {
    public class StartCommand : Command {
        public override Type GetController () {
            return typeof (StartController);
        }
    }

    public class IndexCommand : StartCommand {

    }

}

