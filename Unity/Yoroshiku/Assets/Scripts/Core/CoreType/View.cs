using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ZCore {

    /// <summary>模块视图，只包含游戏物体或资源的引用</summary>
    public abstract class View : MonoBehaviour {
        public void Destroy() {
            Destroy(this.gameObject);
        }
    }

}