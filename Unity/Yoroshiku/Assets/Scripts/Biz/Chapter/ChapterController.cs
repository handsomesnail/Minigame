using System;
using ZCore;
using Biz.Gaming;
using UnityEngine.SceneManagement;
namespace Biz.Chapter {
    public class ChapterController : Controller<GamingModel, GamingView> {
        public void OnSelectChapterCommand (SelectChapterCommand cmd) {
            SceneManager.LoadScene ("Chapter");
            //Scene scene = SceneManager.GetSceneByName ("Chapter");
            //SceneManager.SetActiveScene (scene);
        }
    }
}
