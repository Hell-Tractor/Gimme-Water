using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Mirror;

public class RadarTarget : NetworkBehaviour {
    public Sprite LocalSprite;
    public Color LocalColor;
    public Sprite MVPSprite;
    public Color MVPColor;
    public Sprite RemoteSprite;
    public Color RemoteColor;

    public Color Color {
        get {
            if (isLocalPlayer)
                return LocalColor;
            else if (GameManager.Instance.PlayersWithMaxRemainedWater?.Contains(this.gameObject) == true)
                return MVPColor;
            else
                return RemoteColor;
        }
    }

    public Sprite Sprite {
        get {
            if (isLocalPlayer)
                return LocalSprite;
            else if (GameManager.Instance.PlayersWithMaxRemainedWater?.Contains(this.gameObject) == true)
                return MVPSprite;
            else
                return RemoteSprite;
        }
    }

    private void OnDestroy() {
        if (ImageOnRadar != null)
            Destroy(ImageOnRadar.gameObject);
    }

    [HideInInspector]
    public Image ImageOnRadar = null;
}