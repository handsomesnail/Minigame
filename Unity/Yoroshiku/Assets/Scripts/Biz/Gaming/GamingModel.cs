using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Map;
using Biz.Player;
using UnityEngine;
using ZCore;

namespace Biz.Gaming {
    public sealed class GamingModel : Model {

        #region  MapConfig Data

        /// <summary>本关地图索引</summary>
        public int MapIndex { get; set; }

        /// <summary>加载的地图</summary>
        public BaseMap Map { get; set; }

        #endregion

        #region  Runtime Data

        /// <summary>当前游戏状态</summary>
        public GameStatus GameStatus { get; set; }

        /// <summary>当前所选颜色</summary>
        public int CurrentColorIndex { get; set; }

        /// <summary>当前的溶入状态</summary>
        public bool MeltStatus { get; set; }

        public MeltArea CurrentStayMeltArea { get; set; }

        public bool Jump { get; set; }

        public Vector2 Offset { get; set; }

        #endregion

    }
}