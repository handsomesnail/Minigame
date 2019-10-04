using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Input;
using Biz.Item;
using Biz.Map;
using Biz.Player;
using UnityEngine;
using ZCore;
namespace Biz.Gaming {
    public sealed class GamingView : View {

        public MapList MapList;

        public PlayerSetting PlayerSetting;

        public PlayerAudioSetting AudioSetting;

        public InputView InputView;

        public PlayerView PlayerView;

        [Obsolete]
        public CollectItemList CollectItemList; //没用了？
    }
}