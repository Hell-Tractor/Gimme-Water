using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class RoomUIManager : MonoBehaviour {
    public Text IPText;
    public NetworkManager NetworkManager;
    public InputField IPInputField;
    public Text MessageText;
    private void Start() {
        IPText.text = "IP Address: " + GetLocalIP();
    }
    public static string GetLocalIP() {
        string strHostName = Dns.GetHostName();
        IPHostEntry ipHostEntry = Dns.GetHostEntry(strHostName);
        IPAddress[] addr = ipHostEntry.AddressList;
        string ip = addr[addr.Length - 1].ToString();
        return ip;
    }
    public void CreateHost() {
        this.NetworkManager.StartHost();
    }
    public void CreateClient() {
        this.NetworkManager.StartClient();
        // MessageText.text = "<color=red>Error: Failed to connect to host.</color>";
    }
    public void ShowIPInputField() {
        IPInputField.gameObject.SetActive(true);
        IPInputField.onEndEdit.AddListener((string value) => {
            this.NetworkManager.networkAddress = value;
            IPInputField.gameObject.SetActive(false);
            this.CreateClient();
        });
    }
}