using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
namespace Biz.Map {
    public class RandomFish : MonoBehaviour {
        public GameObject tpl;

        private void Start () {

        }


        private void Awake () {
            StartCoroutine (CreateFishes ());
        }

        private IEnumerator CreateFishes () {
            int size = 10;
            System.Random random = new System.Random ();

            // 创建间隔
            float [] t = new float [size];
            for(int i = 0; i<size; i++) {
                t [i] = (float)random.NextDouble () * 10f;
            }
            Array.Sort (t);
            for (int i = size - 1;  i > 0; i--) {
                t [i] -= t [i - 1];
            }

            if (tpl != null) {
                for (int i = 0; i < size; i++) {
                    float x = (float)random.NextDouble () * 65f - 15f; // [-15, 50]
                    float y = (float)random.NextDouble () * 8f - 7f; // [-7, 1]
                    float scale = (float)(random.NextDouble () + 0.5) / 1.5f;
                    Transform trans = gameObject.transform;
                    Vector3 pos = new Vector3 (x, y, 0);
                    Vector3 sca = new Vector3 (scale, scale, scale);
                    GameObject go = Instantiate (tpl, trans);
                    go.transform.localPosition = pos;
                    go.transform.localScale = sca;
                    go.SetActive (true);

                    yield return new WaitForSeconds (t [i]);
                }
            }
        }
    }
}
