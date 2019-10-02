using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Map;
using Biz.Player;
using Biz.Storage;
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

        /// <summary>当前的溶入状态</summary>
        public bool MeltStatus { get; set; }

        public LinkedList<MeltArea> CurrentStayMeltAreas { get; set; }

        public MeltArea LastExitMeltArea { get; set; }

        public IAttachable AttachedObject { get; set; }

        public bool Jump { get; set; }

        public Vector2 Offset { get; set; }

        public float LastJumpReqTime { get; set; }

        /// <summary>按下溶入键的时间</summary>
        public float LastMeltReqTime { get; set; }

        /// <summary>切换为溶入状态的时间</summary>
        public float LastMeltTime { get; set; }

        /// <summary>切换为正常状态的时间</summary>
        public float LastMeltOutTime { get; set; }

        /// <summary>当前所停留的地面</summary>
        public Collider2D StayedGround { get; set; }

        #endregion

        #region Storage

        /// <summary>该账户当前存档</summary>
        public StoragePoint StoragePoint;

        public string Token;

        public bool IsGuest;

        #endregion

    }
}