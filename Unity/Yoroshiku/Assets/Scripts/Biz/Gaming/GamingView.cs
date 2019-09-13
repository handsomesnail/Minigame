using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Input;
using Biz.Map;
using Biz.Player;
using UnityEngine;
using ZCore;

namespace Biz.Gaming {
    public sealed class GamingView : View {

        public MapList MapList;

        public PlayerSetting PlayerSetting;

        public InputView InputView;

        public PlayerView PlayerView;

    }
}