using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class RadarTarget : NetworkBehaviour {
    public Sprite LocalSprite;
    public Sprite RemoteSprite;
    public Color LocalColor;
    public Color RemoteColor;

    public Color Color {
        get {
            return isLocalPlayer ? LocalColor : RemoteColor;
        }
    }

    public Sprite Sprite {
        get {
            return isLocalPlayer ? LocalSprite : RemoteSprite;
        }
    }

    [HideInInspector]
    public Image ImageOnRadar = null;
}