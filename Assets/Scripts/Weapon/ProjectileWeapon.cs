using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public EnemyDamager Damager;
    public Projectile Projectile;

    private float _shotCounter;

    public float WeaponRange;

    public LayerMask WhatIsEnemy;
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

        _shotCounter -= Time.deltaTime;
        if(_shotCounter <= 0)
        {
            _shotCounter = Stats[WeaponLvl].TimeBetweenAttack;

            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, WeaponRange * Stats[WeaponLvl].Range, WhatIsEnemy);
            if(enemies.Length > 0)
            {
                for (int i = 0; i < Stats[WeaponLvl].Amaunt; i++)
                {
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;

                    Vector3 direction = targetPosition - transform.position;
                    float angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angel -= 90;
                    Projectile.transform.rotation = Quaternion.AngleAxis(angel, Vector3.forward);

                    Instantiate(Projectile, Projectile.transform.position, Projectile.transform.rotation).gameObject.SetActive(true);
                }

                SFXManager.Instance.PlaySfxPitched(6);
            }
        }
    }

    void SetStats()
    {
        Damager.DamageAmout = Stats[WeaponLvl].Damage;
        Damager.LifeTime = Stats[WeaponLvl].Duration;

        Damager.transform.localScale = Vector3.one * Stats[WeaponLvl].Range;
        _shotCounter = 0f;

        Projectile.MoveSpeed = Stats[WeaponLvl].Speed;
    }
}
