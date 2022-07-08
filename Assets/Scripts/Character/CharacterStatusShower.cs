using Mirror;
using UnityEngine.UI;

public class CharacterStatusShower : NetworkBehaviour {
    public CharacterStatus Status;
    public Text NameText;
    public Text WaterCountText;

    private void Start() {
        Status = GetComponent<CharacterStatus>();
    }

    private void OnGUI() {
        NameText.text = Status.Name;
        WaterCountText.text = Status.RemainedWater.ToString();
    }
}
