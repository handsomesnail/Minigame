using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using ZCore;

namespace Biz.Map {
    public class BaseMap : MonoBehaviour {

        /// <summary>虚拟跟踪摄像机</summary>
        public CinemachineVirtualCamera VirtualCamera;

        /// <summary>Splatter渲染器</summary>
        public Renderer SplatterRenderer;

        /// <summary>出生点</summary>
        public Transform BornPoint;

        [Serializable]
        public class MapConfig {

            /// <summary>本关可选颜色(初始颜色为Index0)</summary>
            public Color[] AvailableColors;

        }

        [Serializable]
        public class PlayerConfig {
            public float moveSpeed = 2f;
            public float jumpForce = 200f;
        }

        /// <summary>地图数据配置</summary>
        public MapConfig MapConfigData;

        public PlayerConfig PlayerConfigData;

        //临时的
        private void Start() {
            SplatterRenderer.sortingLayerName = "Splatter";
            SplatterRenderer.sortingOrder = 0;
        }

    }
}