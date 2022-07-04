using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    float _remainShootTime = 0.0f;

    public int waterCost = 1;

    public float shootInterval = 0.1f;

    public float shootSpreadAngle = 10f;

    public GameObject bulletPrefab;

    private CharacterStatus _status;

    private bool _shooting = false;
    

    public void startShooting()
    {
        _shooting = true;
    }

    public void stopShooting()
    {
        _shooting = false;
    }

    private void shoot()
    {
        if(_status != null)
        {
            if(_status.RemainedWater <= waterCost)
                return;
            _status.RemainedWater -= waterCost;
        }

        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Random.Range(-shootSpreadAngle, shootSpreadAngle) + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle));
        bullet.GetComponent<Bullet>().Launch(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _status = GetComponent<CharacterStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_shooting)
        {
            if(_remainShootTime < 0.0f)
            {
                shoot();
                _remainShootTime = shootInterval;
            }
            else
                _remainShootTime -= Time.deltaTime;
        }
    }
}
