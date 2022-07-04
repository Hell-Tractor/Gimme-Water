using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterItem : MonoBehaviour, IItem
{
    private float _remainDecreaseTime = 0.0f;

    public bool enableDecrease = false;

    public float decreaseInterval = 1.0f;

    public int waterAmount = 10;


    public void collectByCharacter(CharacterStatus status) 
    {
        status.RemainedWater += waterAmount;
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
