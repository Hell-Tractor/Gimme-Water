using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WaterSource : NetworkBehaviour
{
    private float _remainSpawnWaterTime = 0.0f;

    public bool spawnEnabled = true;

    public float spawnIntervalMin = 1.0f;

    public float spawnIntervalMax = 2.0f; 

    public float spawnRadiusMin = 0.0f;

    public float spawnRadiusMax = 10.0f;

    public int spawnBatchSizeMin = 1;

    public int spawnBatchSizeMax = 5;

    public int spawnWaterAmountMin = 10;

    public int spawnWaterAmountMax = 10;

    public GameObject waterItemPrefab;

    private Vector3 _getSpawnPosition() {
        float r = Random.Range(spawnRadiusMin, spawnRadiusMax);
        float theta = Random.Range(0.0f, Mathf.Acos(-1.0f) * 2.0f);
        Vector3 pos = new Vector3(r * Mathf.Cos(theta), r * Mathf.Sin(theta));
        pos += this.transform.position;
        return pos;
    }

    public void spawnWaterItem()
    {
        Vector3 pos = _getSpawnPosition();
        int i;
        for (i = 0; Physics2D.OverlapCircle(pos, 2f) != null && i < 30; ++i) {
            pos = _getSpawnPosition();
        }
        if (i == 10) {
            Debug.Log("Failed to find a valid position for water item");
            return;
        }
        var waterItem = Instantiate(waterItemPrefab, pos, Quaternion.identity);
        // waterItem.transform.parent = this.transform;
        waterItem.GetComponent<WaterItem>().waterAmount = Random.Range(
            spawnWaterAmountMin, 
            spawnWaterAmountMax);
        NetworkServer.Spawn(waterItem);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isServer)
        {
            if(spawnEnabled)
            {
                if(_remainSpawnWaterTime <= 0.0f)
                {
                    int size = Random.Range(spawnBatchSizeMin, spawnBatchSizeMax);
                    for(int i = 0; i < size; i++)
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
}
