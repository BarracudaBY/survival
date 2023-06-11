using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{
    public EnemyDamager Damager;

    private float _spawnTime, _spawnCounter;

    private void Start()
    {
        SetStats();
    }

    private void Update()
    {
        if (StatsUpdate == true)
        {
            StatsUpdate = false;

            SetStats();
        }

        _spawnCounter -= Time.deltaTime;
        if(_spawnCounter <= 0f)
        {
            _spawnCounter = _spawnTime;
            Instantiate(Damager, Damager.transform.position, Quaternion.identity, transform).gameObject.SetActive(true);

            SFXManager.Instance.PlaySfxPitched(10);
        }
    }

    void SetStats()
    {
        Damager.DamageAmout = Stats[WeaponLvl].Damage;
        Damager.LifeTime = Stats[WeaponLvl].Duration;
        Damager.TimeBetweenDamage = Stats[WeaponLvl].Speed;
        Damager.transform.localScale = Vector3.one * Stats[WeaponLvl].Range;

        _spawnTime = Stats[WeaponLvl].TimeBetweenAttack;
        _spawnCounter = 0f;
    }
}
