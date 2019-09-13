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
            BaseMap mapPrefab = null;
            if (Model.TryUseTestMap) {
                mapPrefab = View.MapList.TestMap;
            }
            if (mapPrefab == null) {
                mapPrefab = View.MapList.Maps[Model.MapIndex];
            }
            Model.Map = GameObject.Instantiate(mapPrefab.gameObject, View.transform, true).GetComponent<BaseMap>();
            Model.Map.VirtualCamera.Follow = View.PlayerView.Player;
            Model.Map.SplatterRenderer.sortingLayerName = "Splatter";
            Model.Map.SplatterRenderer.sortingOrder = 0;
        }

    }
}