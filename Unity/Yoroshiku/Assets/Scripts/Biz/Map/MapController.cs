using System;
using System.Collections;
using System.Collections.Generic;
using Biz.Gaming;
using UnityEngine;
using ZCore;

namespace Biz.Map {

    public sealed class MapController : Controller<GamingModel, GamingView> {

        /// <summary>加载地图</summary>
        public void OnLoadCommand(LoadCommand cmd) {
            Model.Map = GameObject.Instantiate(View.MapView.Maps[Model.MapIndex], View.transform, true).GetComponent<BaseMap>();
            Model.Map.VirtualCamera.Follow = View.PlayerView.Player;
            Model.Map.SplatterRenderer.sortingLayerName = "Splatter";
            Model.Map.SplatterRenderer.sortingOrder = 0;
        }

    }
}