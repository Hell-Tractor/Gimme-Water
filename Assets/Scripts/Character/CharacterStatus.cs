using UnityEngine;

public class CharacterStatus : MonoBehaviour {
    public float Speed = 6;
    public float DizzyTime = 3;
    public float DizzyTimeReduceAmount = 0.2f;
    public int InitWater;
    [HideInInspector]
    public int RemainedWater = 0;
    [HideInInspector]
    public Vector2 Direction;
    [HideInInspector]
    public IItem ItemCanCollect = null;

    private void Start() {
        RemainedWater = InitWater;
    }
}
