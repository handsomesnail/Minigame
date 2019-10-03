﻿using System;
using ZCore;

namespace Biz.Chapter {
    public class ChapterCommand : Command {
        public override Type GetController () {
            return typeof (ChapterController);
        }
    }

    public class SelectChapterCommand : ChapterCommand {
        public int MapIndex;

        public SelectChapterCommand (int mapIndex) {
            MapIndex = mapIndex;
        }
    }
}
