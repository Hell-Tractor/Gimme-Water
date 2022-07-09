using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Shooter : NetworkBehaviour
{
    float _remainShootTime = 0.0f;

    public int waterCost = 1;

    public float shootInterval = 0.1f;

    public float shootSpreadAngle = 10f;

    public float initialDistance = 0.5f;

    public GameObject bulletPrefab;

    private CharacterStatus _status;

    public bool shooting = false;

    private float _shootAngle = 0.0f;
    
    public Transform ShootingCenter;

    [Command]
    public void CmdChangeShootDirection(float angle)
    {
        _shootAngle = angle;
    }

    public float GetShootAngle()
    {
        return _shootAngle;
    }

    private void shoot()
    {
        if(_status != null)
        {
            if(_status.RemainedWater < waterCost)
                return;
            _status.RemainedWater -= waterCost;
        }

        float angle = Random.Range(-shootSpreadAngle, shootSpreadAngle) + this.GetShootAngle();
        var bullet = Instantiate(bulletPrefab, ShootingCenter.transform.position, transform.rotation);

        bullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle));
        bullet.transform.position += bullet.transform.rotation * Vector3.right * initialDistance;
        bullet.GetComponent<Bullet>().Launch(this);

        NetworkServer.Spawn(bullet);
        bullet.GetComponent<Bullet>().PlayFireSFX();
    }

    // Start is called before the first frame update
    void Start()
    {
        _status = GetComponent<CharacterStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - ShootingCenter.transform.position;
            CmdChangeShootDirection(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }

        if(isServer)
        {
            if(shooting)
            {
                if(_remainShootTime <= 0.0f)
                {
                    shoot();
                    _remainShootTime = shootInterval;
                }
                else
                    _remainShootTime -= Time.deltaTime;
            }
        }
    }
}
