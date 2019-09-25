using System;
using ZCore;
namespace Biz.Help {
    public class HelpCommand: Command {
        public override Type GetController () {
            return typeof (HelpController);
        }
    }
}
