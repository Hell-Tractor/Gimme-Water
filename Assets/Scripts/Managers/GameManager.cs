using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public enum GameState {
    UNSTARTED,
    RUNNING,
    ALMOST_END,
    ENDED
}

public class GameManager : MonoBehaviour {
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    public AudioSource BGMSource;
    public AudioSource SFXSource;
    public GameState GameState = GameState.UNSTARTED;
    public float GameDuration;
    public float EndingTime;
    public GameObject SettlementUIPrefab;
    public int NeedPlayerCount = 1;
    public int CurrentPlayerCount {
        get {
            return GameObject.FindGameObjectsWithTag("Player").Length;
        }
    }
    private float _remainTime;
    public float RemainTime { get { return _remainTime; }}
    [HideInInspector]
    public GameObject[] PlayersWithMaxRemainedWater;
    [HideInInspector]
    public string currentPlayerName;
    public AudioClip GameBgm;

    public void StartGame() {
        _remainTime = GameDuration;
        GameState = GameState.RUNNING;

        WaterSource[] waterSources = GameObject.FindObjectsOfType<WaterSource>();
        foreach (WaterSource waterSource in waterSources) {
            waterSource.spawnEnabled = true;
        }
    }
    public void OnGameEnd() {
        WaterSource[] waterSources = GameObject.FindObjectsOfType<WaterSource>();
        foreach (WaterSource waterSource in waterSources) {
            waterSource.spawnEnabled = false;
        }

        GameObject settlementUI = Instantiate(SettlementUIPrefab);
        settlementUI.GetComponent<SettlementManager>().Show();
        Time.timeScale = 0;
    }
    private void _getMVPPlayers() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
        int maxWater = objects.Max(obj => obj.GetComponent<CharacterStatus>().RemainedWater);
        PlayersWithMaxRemainedWater = objects.Where(obj => obj.GetComponent<CharacterStatus>().RemainedWater == maxWater).ToArray();
    }
    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            Debug.LogWarning("Duplicate GameManager detected. Destroying...");
        } else {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start() {
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => {
            if (scene.name == "GameScene") {
                BGMSource.Stop();
                BGMSource.clip = GameBgm;
                BGMSource.Play();
            }
        };
    }
    private void Update() {
        if (GameState == GameState.UNSTARTED && NeedPlayerCount == CurrentPlayerCount)
            this.StartGame();
        if (GameState == GameState.RUNNING || GameState == GameState.ALMOST_END) {
            _remainTime -= Time.deltaTime;
            _getMVPPlayers();
            if (GameState == GameState.RUNNING && _remainTime <= EndingTime) {
                GameState = GameState.ALMOST_END;
            } else if (GameState == GameState.ALMOST_END && _remainTime <= 0) {
                GameState = GameState.ENDED;
                this.OnGameEnd();
            }
        }
    }
}
