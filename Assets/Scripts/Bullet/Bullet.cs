using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Bullet : NetworkBehaviour
{
    public float lifeTime = 1.0f;

    public float initialSpeed = 1.0f;

    public AnimationCurve speedCurve;
    public AudioClip OnFireSound;

    float _remainTime = 0.0f;

    private Rigidbody2D _rigidbody2D;
    
    private Shooter _shooter;


    public void Launch(Shooter shooter)
    {
        _shooter = shooter;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = Vector2.right;
        _rigidbody2D.velocity = transform.rotation * _rigidbody2D.velocity;
        _remainTime = lifeTime;
    }

    // Start is called before the first frame update
    void Start()
    {   
        _rigidbody2D = GetComponent<Rigidbody2D>();
        this.PlayFireSFX();
    }

    void FixedUpdate()
    {
        if(isServer)
        {
            float t = _remainTime / lifeTime;
            _rigidbody2D.velocity = _rigidbody2D.velocity.normalized;
            _rigidbody2D.velocity *= speedCurve.Evaluate(t) * initialSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isServer)
        {
            _remainTime -= Time.deltaTime;

            if(_remainTime <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject != _shooter.gameObject) {
            if (collision.CompareTag("Player"))
                collision.GetComponent<AI.FSM.CharacterFSM>()?.SetTrigger(AI.FSM.FSMTriggerID.UnderAttack);
            Destroy(gameObject);
        }
    }

    [ClientRpc]
    public void PlayFireSFX() {
        if (GameManager.Instance.SFXSource.isPlaying)
            GameManager.Instance.SFXSource.Stop();
        GameManager.Instance.SFXSource.PlayOneShot(OnFireSound);
    }
}
