using Es.InkPainter;
using UnityEngine;

namespace Biz.Map.Splatter {

    [RequireComponent(typeof(Camera))]
    public class MouseSplatterCreator : MonoBehaviour {

        /// <summary>
        /// Types of methods used to paint.
        /// </summary>
        [System.Serializable]
        private enum UseMethodType {
            RaycastHitInfo,
            WorldPoint,
            NearestSurfacePoint,
            DirectUV,
        }

        [SerializeField]
        private Brush brush;

        [SerializeField]
        private UseMethodType useMethodType = UseMethodType.RaycastHitInfo;

        [SerializeField]
        bool erase = false;

        private int layerMask = 0;
        private Camera usedCamera;

        private void Start() {
            layerMask = 1 << LayerMask.NameToLayer("Splatter");
            usedCamera = GetComponent<Camera>();
        }

        private void Update() {
            if (UnityEngine.Input.GetMouseButton(0)) {
                var ray = usedCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
                bool success = true;
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, 20, layerMask)) {
                    InkCanvas paintObject = hitInfo.transform.GetComponent<InkCanvas>();
                    if (paintObject != null) {
                        //success = paintObject.PaintNearestTriangleSurface(brush, hitInfo.point);
                        //canvas.ResetPaint() 清空Splatter
                        switch (useMethodType) {
                            case UseMethodType.RaycastHitInfo:
                                success = erase ? paintObject.Erase(brush, hitInfo) : paintObject.Paint(brush, hitInfo);
                                break;

                            case UseMethodType.WorldPoint:
                                success = erase ? paintObject.Erase(brush, hitInfo.point) : paintObject.Paint(brush, hitInfo.point);
                                break;

                            case UseMethodType.NearestSurfacePoint:
                                success = erase ? paintObject.EraseNearestTriangleSurface(brush, hitInfo.point) : paintObject.PaintNearestTriangleSurface(brush, hitInfo.point);
                                break;

                            case UseMethodType.DirectUV:
                                if (!(hitInfo.collider is MeshCollider))
                                    Debug.LogWarning("Raycast may be unexpected if you do not use MeshCollider.");
                                success = erase ? paintObject.EraseUVDirect(brush, hitInfo.textureCoord) : paintObject.PaintUVDirect(brush, hitInfo.textureCoord);
                                break;
                        }
                    }
                    if (!success) {
                        Debug.LogError("Failed to paint.");
                    }
                }
            }
        }

        public void OnGUI() {
            if (GUILayout.Button("Reset")) {
                foreach (var canvas in FindObjectsOfType<InkCanvas>())
                    canvas.ResetPaint();
            }
        }

    }
}