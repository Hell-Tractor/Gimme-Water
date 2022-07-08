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
    public bool IsPlayer;

    public Color Color {
        get {
            if (!IsPlayer)
                return LocalColor;
            if (isLocalPlayer)
                return LocalColor;
            else if (GameManager.Instance.PlayersWithMaxRemainedWater.Contains(this.gameObject))
                return MVPColor;
            else
                return RemoteColor;
        }
    }

    public Sprite Sprite {
        get {
            if (!IsPlayer)
                return LocalSprite;
            if (isLocalPlayer)
                return LocalSprite;
            else if (GameManager.Instance.PlayersWithMaxRemainedWater.Contains(this.gameObject))
                return MVPSprite;
            else
                return RemoteSprite;
        }
    }

    [HideInInspector]
    public Image ImageOnRadar = null;
}