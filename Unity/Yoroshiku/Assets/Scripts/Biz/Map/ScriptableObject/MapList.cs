using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Map {

    [CreateAssetMenu(menuName = "ScriptableObject/MapList")]
    public class MapList : ScriptableObject {
        public BaseMap TestMap;
        public BaseMap[] Maps;
    }
}