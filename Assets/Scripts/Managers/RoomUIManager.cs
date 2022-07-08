using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class RoomUIManager : MonoBehaviour {
    public Text IPText;
    public NetworkManager NetworkManager;
    public DialogManager Dialog;
    public Text MessageText;
    private void Start() {
        IPText.text = "IP Address: " + GetLocalIP();
        Dialog.gameObject.SetActive(false);
    }
    public static string GetLocalIP() {
        string strHostName = Dns.GetHostName();
        IPHostEntry ipHostEntry = Dns.GetHostEntry(strHostName);
        IPAddress[] addr = ipHostEntry.AddressList;
        string ip = addr[addr.Length - 1].ToString();
        return ip;
    }
    public void CreateHost() {
        Dialog.gameObject.SetActive(true);
        Dialog.Show("玩家姓名：", "玩家数量：", (name, count) => {
            if (name.Length < 0) {
                MessageText.text = "玩家姓名不能为空";
                return false;
            }
            if (int.TryParse(count, out int c) && c > 0 && c < this.NetworkManager.maxConnections) {
                GameManager.Instance.currentPlayerName = name;
                GameManager.Instance.NeedPlayerCount = c;
                this.NetworkManager.StartHost();
                return true;
            } else {
                MessageText.text = "<color=red>玩家数量必须大于0且小于" + this.NetworkManager.maxConnections + "</color>";
                return false;
            }
        });
    }
    public void CreateClient() {
        Dialog.gameObject.SetActive(true);
        Dialog.Show("玩家姓名：", "IP地址：", (name, ip) => {
            if (name.Length < 0) {
                MessageText.text = "玩家姓名不能为空";
                return false;
            }
            if (ip.Length > 0) {
                GameManager.Instance.currentPlayerName = name;
                this.NetworkManager.networkAddress = ip;
                this.NetworkManager.StartClient();
                return true;
            } else {
                MessageText.text = "<color=red>IP地址不能为空</color>";
                return false;
            }
        });
    }
}