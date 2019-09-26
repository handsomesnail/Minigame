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
            MapList mapList = Resources.Load<MapList>("Configs/MapList");
            if (!string.IsNullOrEmpty(Main.DebugMapName) && Main.DebugMapName == mapList.TestMap.gameObject.name) {
                mapPrefab = mapList.TestMap.gameObject.GetComponent<BaseMap>();
            }
            else {
                mapPrefab = View.MapList.Maps[Model.MapIndex];
            }
            Model.Map = GameObject.Instantiate(mapPrefab.gameObject, View.transform, true).GetComponent<BaseMap>();
            Model.Map.VirtualCamera.Follow = View.PlayerView.Player;
        }

    }
}