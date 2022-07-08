using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SettlementManager : MonoBehaviour {
    public Transform SettlementGroup;
    public GameObject SettlementPrefab;
    public GameObject SettlementUI;
    public Sprite FirstPlaceSprite;
    public Sprite OtherPlaceSprite;

    private void Start() {
        SettlementUI.SetActive(false);
    }

    public void Show() {
        CharacterStatus[] players = GameObject.FindObjectsOfType<CharacterStatus>().OrderByDescending(obj => obj.RemainedWater).ToArray();
        foreach (CharacterStatus player in players) {
            GameObject settlement = Instantiate(SettlementPrefab, SettlementGroup);
            settlement.GetComponent<PlayerInfoShower>().Show(player, player.RemainedWater == players[0].RemainedWater ? FirstPlaceSprite : OtherPlaceSprite);
        }
        
        SettlementUI.SetActive(true);
    }
}
