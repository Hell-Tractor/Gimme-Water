using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 1.0f;

    public float initialSpeed = 1.0f;

    public AnimationCurve speedCurve;

    float _remainTime = 0.0f;

    private Rigidbody2D _rigidbody2D;

    public void Launch()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = Vector2.right;
        _rigidbody2D.velocity = transform.rotation * _rigidbody2D.velocity;
        _remainTime = lifeTime;
    }

    // Start is called before the first frame update
    void Start()
    {   
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float t = _remainTime / lifeTime;
        _rigidbody2D.velocity = _rigidbody2D.velocity.normalized;
        _rigidbody2D.velocity *= speedCurve.Evaluate(t) * initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        _remainTime -= Time.deltaTime;

        if(_remainTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
