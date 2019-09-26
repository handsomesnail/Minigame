using System;
using System.Collections.Generic;
using UnityEngine;
namespace Biz.Item {
    [CreateAssetMenu (menuName = "ScriptableObject/CollectItemList")]
    public class CollectItemList : ScriptableObject {
        public List<Item> Items;
    }
}
