using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZCore;

namespace Biz.Storage {

    [CreateAssetMenu (menuName = "ScriptableObject/MapList")]
    public class StorageFlagList : ScriptableObject {
        public StorageFlag [] StorageFlags;
    }
}