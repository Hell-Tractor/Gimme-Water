using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Mirror;

public class SettlementManager : MonoBehaviour {
    public Transform SettlementGroup;
    public GameObject SettlementPrefab;
    public Sprite FirstPlaceSprite;
    public Sprite OtherPlaceSprite;

    public void Show() {
        CharacterStatus[] players = GameObject.FindObjectsOfType<CharacterStatus>().OrderByDescending(obj => obj.RemainedWater).ToArray();
        foreach (CharacterStatus player in players) {
            GameObject settlement = Instantiate(SettlementPrefab, SettlementGroup);
            settlement.GetComponent<PlayerInfoShower>().Show(player, player.RemainedWater == players[0].RemainedWater ? FirstPlaceSprite : OtherPlaceSprite);
        }
    }

    public void ExitGame() {
        Disconnect();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Replay() {
        Time.timeScale = 1;
        Disconnect();
    }

    public void Disconnect() {
        NetworkManager manager = NetworkManager.singleton;
        if (NetworkServer.active && NetworkClient.isConnected) {
            manager.StopHost();
        } else if (NetworkClient.isConnected) {
            manager.StopClient();
        } else if (NetworkServer.active) {
            manager.StopServer();
        }
    }
}
