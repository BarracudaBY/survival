using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineWeapon : Weapon
{
    public float RotateSpeed;
    public Transform Holder, FireBallToSpawn;
    public float TimeBetweenToSpawn;

    private float SpawnCounter;
    public EnemyDamager Damager;

    private void Start()
    {
        SetStats();

        //UIController.Instance.LevelUpButtons[0].UpdateButtonDispay(this);
    }

    private void Update()
    {
        //Holder.rotation = Quaternion.Euler(0f, 0f, Holder.rotation.eulerAngles.z + (RotateSpeed * Time.deltaTime));
        Holder.rotation = Quaternion.Euler(0f, 0f, Holder.rotation.eulerAngles.z + (RotateSpeed * Time.deltaTime * Stats[WeaponLvl].Speed));

        SpawnCounter -= Time.deltaTime;

        if (SpawnCounter <= 0)
        {
            SpawnCounter = TimeBetweenToSpawn;

            //Instantiate(FireBallToSpawn, FireBallToSpawn.position, FireBallToSpawn.rotation, Holder).gameObject.SetActive(true);

            for (int i = 0; i < Stats[WeaponLvl].Amaunt; i++)
            {
                float rotate = (360f / Stats[WeaponLvl].Amaunt) * i;

                Instantiate(FireBallToSpawn, FireBallToSpawn.position, Quaternion.Euler(0f,0f,rotate), Holder).gameObject.SetActive(true);

                SFXManager.Instance.PlaySfx(8);

            }
        }

        if (StatsUpdate == true)
        {
            StatsUpdate = false;

            SetStats();
        }
    }

    public void SetStats()
    {
        Damager.DamageAmout = Stats[WeaponLvl].Damage;

        transform.localScale = Vector3.one * Stats[WeaponLvl].Range;

        TimeBetweenToSpawn = Stats[WeaponLvl].TimeBetweenAttack;

        Damager.LifeTime = Stats[WeaponLvl].Duration;

        SpawnCounter = 0f;
        

    }
}
