using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoShower : MonoBehaviour {
    public Image BackgroundImage;
    public Text PlayerName;
    public Text PlayerWater;

    public void Show(CharacterStatus status, Sprite BackgroundSprite) {
        PlayerName.text = status.Name;
        PlayerWater.text = status.RemainedWater.ToString();
        BackgroundImage.sprite = BackgroundSprite;
    }
}
