using UnityEngine;

public class CharacterController : MonoBehaviour {
    public float speed = 6.0f;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate() {
        _rigidbody.velocity = _moveDirection * speed;
    }
}