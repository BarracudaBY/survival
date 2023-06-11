using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float DamageAmout;
    public float LifeTime, GrowSpeed = 5f;
    public bool ShouldKnockBack;
    public bool DestroyParent;

    public bool DamageOverTime;
    public float TimeBetweenDamage;
    private float _damageCounter;

    private List<EnemyController> _enemiesInRange = new List<EnemyController>();

    private Vector3 _targetSize;

    public bool DestroyOnImpact;


    private void Start()
    {
        //Destroy(gameObject, LifeTime);

        _targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, _targetSize, GrowSpeed * Time.deltaTime);

        LifeTime -= Time.deltaTime;
        if(LifeTime <= 0)
        {
            _targetSize = Vector3.zero;
            if(transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                if (DestroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        if(DamageOverTime == true)
        {
            _damageCounter -= Time.deltaTime;

            if(_damageCounter <= 0)
            {
                _damageCounter = TimeBetweenDamage;

                for (int i = 0; i < _enemiesInRange.Count; i++)
                {
                    if(_enemiesInRange[i] != null)
                    {
                        _enemiesInRange[i].TakeDamage(DamageAmout, ShouldKnockBack);
                    }
                    else
                    {
                        _enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(DamageOverTime == false)
        {
            if(collision.tag == "Enemy")
            {
                collision.GetComponent<EnemyController>().TakeDamage(DamageAmout, ShouldKnockBack);

                if (DestroyOnImpact)
                {
                    Destroy(gameObject);
                }
            }

        }
        else
        {
            if(collision.tag == "Enemy")
            {
                _enemiesInRange.Add(collision.GetComponent<EnemyController>());
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(DamageOverTime == true)
        {
            if(collision.tag == "Enemy")
            {
                _enemiesInRange.Remove(collision.GetComponent<EnemyController>());
            }
        }
    }
}
