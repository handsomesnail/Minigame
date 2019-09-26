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

        /// <summary>出生点</summary>
        public Transform BornPoint;

    }
}