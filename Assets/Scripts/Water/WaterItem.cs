using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WaterItem : Collectable
{
    private float _remainDecreaseTime = 0.0f;

    public bool enableDecrease = false;

    public float decreaseInterval = 1.0f;

    public int waterAmount = 10;
    public AudioClip CollectSound;

    override public void collectByCharacter(CharacterStatus status) 
    {
        status.RemainedWater += waterAmount;

        this.GetComponent<Animator>().SetTrigger("CollectTrigger");

        Destroy(this.GetComponent<Collider2D>());
        Destroy(this.gameObject, 0.75f);
    }

    void Update()
    {
        if(isServer)
        {
            if(enableDecrease)
            {
                if(_remainDecreaseTime <= 0.0f) 
                {   
                    if(--waterAmount <= 0)
                    {
                        Destroy(gameObject);
                    }
                    _remainDecreaseTime = decreaseInterval;
                } 
                else
                    _remainDecreaseTime -= Time.deltaTime;
            }
        }
    }

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<AI.FSM.CharacterFSM>()?.SetTrigger(AI.FSM.FSMTriggerID.ItemFound);
            collision.GetComponent<CharacterStatus>().ItemCanCollect = this;
            this.PlayCollectSFX();
        }
    }

    [ClientRpc]
    private void PlayCollectSFX() {
        GameManager.Instance.SFXSource.PlayOneShot(CollectSound);
    }
}
