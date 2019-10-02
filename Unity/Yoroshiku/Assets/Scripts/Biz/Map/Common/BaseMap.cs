using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Player;
using Cinemachine;
using UnityEngine;
using ZCore;

namespace Biz.Map {
    public class BaseMap : MonoBehaviour {

        /// <summary>虚拟跟踪摄像机</summary>
        public CinemachineVirtualCamera VirtualCamera;

        /// <summary>出生点</summary>
        public Transform BornPoint;

        /// <summary>主角附加颜色(场景色调)</summary>
        public Color PlayerColor = Color.white;

        /// <summary>当前地图人物设置</summary>
        public PlayerSetting ApplyPlayerSetting;

    }
}