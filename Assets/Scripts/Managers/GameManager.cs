using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public AudioSource BGMSource;
    public AudioSource SFXSource;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            Debug.LogWarning("Duplicate GameManager detected. Destroying...");
        } else {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
