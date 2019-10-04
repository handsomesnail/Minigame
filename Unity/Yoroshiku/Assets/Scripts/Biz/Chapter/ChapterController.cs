using System;
using ZCore;
using Biz.Gaming;
using UnityEngine.UI;
using UnityEngine;

namespace Biz.Chapter {
    public class ChapterController : Controller<GamingModel, ChapterView> {
        public void OnChapterCommand (ChapterCommand cmd) {

            //View.Back.GetComponent<Image> ().alphaHitTestMinimumThreshld = 0.1f;
            View.Back.onClick.AddListener (delegate {
                View.Destroy ();
                Call (new Biz.Start.StartCommand ());
            });

            foreach (var item in View.ChapterButtons) {
                if (item != null) InitButton (item, Model.StoragePoint == null? 0: Model.StoragePoint.PassChapter);
            }

        }

        public void OnSelectChapterCommand (SelectChapterCommand cmd) {
            View.Destroy ();
            Call (new Biz.Gaming.EnterCommand (cmd.MapIndex));
        }

        private void InitButton (ChapterButton button, int maxChapter = 0) {
            if (button.MapIndex < maxChapter) {
                Debug.Log ("<");
                button.GetComponent<Image> ().sprite = button.EnabledSprite;
                button.GetComponent<Image> ().material = null;
                button.GetComponent<Button> ().onClick.AddListener (delegate {
                    View.Destroy ();
                    Call (new Biz.Gaming.EnterCommand (button.MapIndex));
                });
            } else if (button.MapIndex == maxChapter) {
                Debug.Log ("=");
                button.GetComponent<Image> ().sprite = button.DisabledSprite;
                button.GetComponent<Image> ().material = null;
                button.transform.localScale = new Vector3 {
                    x = 1.22f,
                    y = 1.22f
                };
                button.GetComponent<Button> ().onClick.AddListener (delegate {
                    View.Destroy ();
                    Call (new Biz.Gaming.EnterCommand (button.MapIndex));
                });
            } else {
                Debug.Log (">");
                button.GetComponent<Image> ().sprite = button.DisabledSprite;
                button.GetComponent<Image> ().material = View.DisabledMaterial;
            }

            button.GetComponent<Image> ().alphaHitTestMinimumThreshold = 0.1f;
            button.GetComponent<RectTransform> ().sizeDelta = new Vector2 {
                x = button.GetComponent<Image> ().sprite.bounds.size.x * button.GetComponent<Image> ().sprite.pixelsPerUnit,
                y = button.GetComponent<Image> ().sprite.bounds.size.y * button.GetComponent<Image> ().sprite.pixelsPerUnit 
            };
        }
    }
}
