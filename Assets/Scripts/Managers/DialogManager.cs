using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    public Text Hint1;
    public InputField InputField1;
    public Text Hint2;
    public InputField InputField2;
    public GameObject HostButton;
    public GameObject JoinButton;

    private Func<string, string, bool> _onConfirm = null;
    
    private void OnEnable() {
        HostButton.SetActive(false);
        JoinButton.SetActive(false);
    }

    private void OnDisable() {
        HostButton.SetActive(true);
        JoinButton.SetActive(true);
    }
    
    public void Show(string hint1, string hint2, Func<string, string, bool> onConfirm) {
        this.Hint1.text = hint1;
        this.Hint2.text = hint2;
        this.InputField1.text = "";
        this.InputField2.text = "";
        this._onConfirm = onConfirm;
    }

    public void Confirm() {
        if (this._onConfirm?.Invoke(this.InputField1.text, this.InputField2.text) == true)
            this.gameObject.SetActive(false);
    }

    public void Close() {
        this.gameObject.SetActive(false);
    }
}
