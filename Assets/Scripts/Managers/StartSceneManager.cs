using UnityEngine;

public class StartSceneManager : MonoBehaviour {
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("RoomScene");
        }
    }
}
