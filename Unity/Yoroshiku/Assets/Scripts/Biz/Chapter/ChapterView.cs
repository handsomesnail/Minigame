using System;
using ZCore;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

namespace Biz.Chapter {
    public class ChapterView : View {
        public Material DisabledMaterial;

        public Button UnlockAll;

        public Button Back;

        public List<ChapterButton> ChapterButtons;
    }
}
