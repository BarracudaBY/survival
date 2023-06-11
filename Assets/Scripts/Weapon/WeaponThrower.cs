using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrower : Weapon
{

    public EnemyDamager Damager;

    private float _throwCounter;
    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (StatsUpdate == true)
        {
            StatsUpdate = false;

            SetStats();
        }

        _throwCounter -= Time.deltaTime;

        if(_throwCounter <= 0)
        {
            _throwCounter = Stats[WeaponLvl].TimeBetweenAttack;

            for (int i = 0; i < Stats[WeaponLvl].Amaunt; i++)
            {
                Instantiate(Damager, Damager.transform.position, Damager.transform.rotation).gameObject.SetActive(true);
            }

            SFXManager.Instance.PlaySfxPitched(4);
        }
    }

    void SetStats()
    {
        Damager.DamageAmout = Stats[WeaponLvl].Damage;
        Damager.LifeTime = Stats[WeaponLvl].Duration;

        Damager.transform.localScale = Vector3.one * Stats[WeaponLvl].Range;

        _throwCounter = 0f;
    }
}
