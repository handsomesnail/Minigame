using System;
using ZCore;
using UnityEngine;
using UnityEngine.UI;
namespace Biz.Chapter {

    [RequireComponent(typeof(Button))]
    public class ChapterButton : MonoBehaviour  {
        public Sprite EnabledSprite;
        public Sprite DisabledSprite;
        public int MapIndex;
    }

}
