using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : MonoBehaviour
{
    // Start is called before the first frame update

    private float _remainSpawnWaterTime = 0.0f;

    public bool spawn_enabled = true;

    public float spawnIntervalMin = 1.0f;

    public float spawnIntervalMax = 2.0f; 

    public float spawnRadiusMin = 0.0f;

    public float spawnRadiusMax = 10.0f;

    public WaterItem waterItemPrefab;


    public void spawnWaterItem()
    {
        float r = Random.Range(spawnIntervalMin, spawnIntervalMax);
        float theta = Random.Range(0.0f, Mathf.Acos(-1.0f) * 2.0f);
        Vector3 pos = new Vector3(r * Mathf.Cos(theta), r * Mathf.Sin(theta));
        pos += transform.position;
        
        Instantiate(waterItemPrefab, pos, Quaternion.identity);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawn_enabled)
        {
            if(_remainSpawnWaterTime < 0.001f)
            {
                spawnWaterItem();
                _remainSpawnWaterTime = Random.Range(
                    spawnIntervalMin, 
                    spawnIntervalMax);
            }
            else
                _remainSpawnWaterTime -= Time.deltaTime;
        }
    }
}
