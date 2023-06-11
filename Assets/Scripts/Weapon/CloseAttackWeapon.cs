using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackWeapon : Weapon
{
    public EnemyDamager Damager;

    private float _attackCounter, _direction;

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

        _attackCounter -= Time.deltaTime;

        if(_attackCounter <= 0)
        {
            _attackCounter = Stats[WeaponLvl].TimeBetweenAttack;

            _direction = Input.GetAxisRaw("Horizontal");

            if (_direction != 0)
            {
                if(_direction > 0)
                {
                    Damager.transform.rotation = Quaternion.identity;
                }
                else
                {
                    Damager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                }

                
            }
            
            Instantiate(Damager, Damager.transform.position, Damager.transform.rotation, transform).gameObject.SetActive(true);

            for (int i = 0; i < Stats[WeaponLvl].Amaunt; i++)
            {
                float rotate = (360f / Stats[WeaponLvl].Amaunt) * i;

                Instantiate(Damager, Damager.transform.position, Quaternion.Euler(0f, 0f, Damager.transform.rotation.eulerAngles.z + rotate), transform).gameObject.SetActive(true);

            }

            SFXManager.Instance.PlaySfxPitched(9);


        }
    }

    void SetStats()
    {
        Damager.DamageAmout = Stats[WeaponLvl].Damage;
        Damager.LifeTime = Stats[WeaponLvl].Duration;

        Damager.transform.localScale = Vector3.one * Stats[WeaponLvl].Range;

        _attackCounter = 0f;
    }
}
