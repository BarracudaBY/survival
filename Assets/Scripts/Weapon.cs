using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<WeaponStats> Stats;
    public int WeaponLvl;

    [HideInInspector]
    public bool StatsUpdate;

    public Sprite Icon;

    public void LevelUp()
    {
        if(WeaponLvl < Stats.Count - 1){
            WeaponLvl++;

            StatsUpdate = true;

            if(WeaponLvl >= Stats.Count - 1)
            {
                PlayerController.Instance.FullyLeveledWeapons.Add(this);
                PlayerController.Instance.AssignetWeapon.Remove(this);
            }
        }
    }

}

[System.Serializable]
public class WeaponStats
{
    public float Speed, Range, Damage, TimeBetweenAttack, Amaunt, Duration;
    public string Upgratetext;
}
