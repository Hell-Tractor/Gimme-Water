using UnityEngine;
using UnityEngine.UI;

public class RadarManager : MonoBehaviour {
    public GameObject RadarTargetIconPrefab;
    public Camera MinimapCamera;

    private void LateUpdate() {
        RadarTarget[] targets = GameObject.FindObjectsOfType<RadarTarget>();
        foreach (RadarTarget target in targets) {
            if (target.ImageOnRadar == null) {
                GameObject radarTarget = Instantiate(RadarTargetIconPrefab, this.transform);
                target.ImageOnRadar = radarTarget.GetComponent<Image>();

                target.ImageOnRadar.color = target.Color;
                target.ImageOnRadar.sprite = target.Sprite;
            }
            _updatePosition(target);
        }
    }

    private void _updatePosition(RadarTarget target) {
        Vector3 pointInViewport = MinimapCamera.WorldToViewportPoint(target.transform.position);
        pointInViewport.x = Mathf.Clamp(pointInViewport.x, 0, 1);
        pointInViewport.y = Mathf.Clamp(pointInViewport.y, 0, 1);

        Vector2 pivot = this.GetComponent<RectTransform>().pivot;
        Vector2 size = this.GetComponent<RectTransform>().sizeDelta;
        Matrix4x4 viewportMatrix = Matrix4x4.identity;
        viewportMatrix[0, 0] = size.x;
        viewportMatrix[0, 3] = -pivot.x * size.x;
        viewportMatrix[1, 1] = size.y;
        viewportMatrix[1, 3] = -pivot.y * size.y;
        
        target.ImageOnRadar.rectTransform.localPosition = viewportMatrix * new Vector4(pointInViewport.x, pointInViewport.y, 0, 1);
    }
}
