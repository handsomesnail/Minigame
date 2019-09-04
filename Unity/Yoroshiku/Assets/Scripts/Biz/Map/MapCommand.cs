using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Map {
    public abstract class MapCommand : Command {
        public override Type GetController() {
            return typeof(MapController);
        }
    }

    /// <summary>加载地图</summary>
    public class LoadCommand : MapCommand { }

}