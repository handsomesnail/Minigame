using System;
using ZCore;

namespace Biz.Account {
    public class AccountCommand : Command {
        public override Type GetController () {
            return typeof (AccountController);
        }
    }

    public class IndexCommand : AccountCommand {

    }

}

