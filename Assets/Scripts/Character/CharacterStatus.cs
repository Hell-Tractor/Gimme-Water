using UnityEngine;

public class CharacterStatus : MonoBehaviour {
    public float Speed = 6;
    [HideInInspector]
    public int RemainedWater = 0;
    [HideInInspector]
    public Vector2 Direction;
    [HideInInspector]
    public IItem ItemCanCollect = null;
}
