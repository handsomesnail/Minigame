using System;
using UnityEngine;
using UnityEngine.UI;
using Biz.Item;
using ZCore;
namespace Biz.Pause {
    public class PauseView : View {
        public Button ContinueButton;
        public Button RestartButton;
        public Button HomeButton;

        public Text ItemText;
        public GridLayoutGroup ItemContainer;

        public CollectItemList ItemList;
    }
}
