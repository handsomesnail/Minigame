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
            // #if UNITY_EDITOR
            //             MapList mapList = Resources.Load<MapList>("Configs/MapList");
            //             if (!string.IsNullOrEmpty(Main.DebugMapName) && Main.DebugMapName == mapList.TestMap.gameObject.name) {
            //                 Main.DebugMapName = string.Empty;
            //                 mapPrefab = mapList.TestMap.gameObject.GetComponent<BaseMap>();
            //             }
            // #else
            if (false) { }
            //#endif
            else {
                string path = "BuildInMaps/0.Teaching";
                switch (Model.MapIndex) {
                    case 0:
                        path = "BuildInMaps/0.Teaching";
                        break;
                    case 1:
                        path = "BuildInMaps/1.Windows";
                        break;
                    case 2:
                        path = "BuildInMaps/1.Window Wasted";
                        break;
                    case 3:
                        path = "BuildInMaps/4.Aquarium";
                        break;
                    case 4:
                        path = "BuildInMaps/4.Aquarium Wasted";
                        break;
                    case 5:
                        path = "BuildInMaps/2.Kitchen";
                        break;
                    case 6:
                        path = "BuildInMaps/2.Kitchen Wasted";
                        break;
                    case 7:
                        path = "BuildInMaps/3.Sofa";
                        break;
                    case 8:
                        path = "BuildInMaps/3.Sofa Wasted";
                        break;
                }
                // #if UNITY_EDITOR
                //                 mapPrefab = mapList.Maps[Model.MapIndex];
                // #else
                mapPrefab = Resources.Load<GameObject>(path).GetComponent<BaseMap>();
                //#endif
            }
            Model.Map = GameObject.Instantiate(mapPrefab.gameObject, View.transform, true).GetComponent<BaseMap>();
            Model.Map.VirtualCamera.Follow = View.PlayerView.Player;
            if (Model.Map.ApplyPlayerSetting != null) {
                View.PlayerSetting = Model.Map.ApplyPlayerSetting;
            }
        }

    }
}