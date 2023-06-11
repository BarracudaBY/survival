using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _moveSpeed;

    public float Damage;
    public float HitWaitTime = 1f;
    public float Health = 5f;
    public float KnockBackTime = .5f;

    private Transform _target;
    private float _hitCounter;
    private float _knockBackCounter;

    public int ExpToGive = 1;
    public int CoinValue = 1;
    public int BenzakToGive = 1;
    public float CoinDropRate = .5f;
    public float BenzakDropRate = 0.1f;

    public GameObject Blood;

    public SpriteRenderer spriteRenderer;
    private GhostEnemyController _ghostController;

    private void Start()
    {
        //_target = FindAnyObjectByType<PlayerController>().transform;

        _target = PlayerHealthController.Instance.transform;

        spriteRenderer = GetComponent<SpriteRenderer>();
        _ghostController = GetComponent<GhostEnemyController>();
        _ghostController.enabled = false;
    }

    private void Update()
    {
        if(PlayerController.Instance.gameObject.activeSelf == true)
        {
            if (_knockBackCounter > 0)
            {
                _knockBackCounter -= Time.deltaTime;

                if (_moveSpeed > 0)
                {
                    _moveSpeed = -_moveSpeed * 2f;
                }

                if (_knockBackCounter <= 0)
                {
                    _moveSpeed = Mathf.Abs(_moveSpeed * 0.5f);
                }
            }

            _rigidbody.velocity = (_target.position - transform.position).normalized * _moveSpeed;

            if (_hitCounter > 0)
            {
                _hitCounter -= Time.deltaTime;
            }

            _ghostController.enabled = true;


        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
            _ghostController.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && _hitCounter <= 0)
        {
            PlayerHealthController.Instance.TakeDamage(Damage);
            //StopAllCoroutines();

            //StartCoroutine(FindObjectOfType<PlayerHealthController>().TakeDamage(Damage));

            _hitCounter = HitWaitTime;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        Health -= damageToTake;
        

        if (Health <= 0)
        {
            Destroy(gameObject);
            ExpLvlController.Instance.SpawnExp(transform.position, ExpToGive);

            if (Random.value <= CoinDropRate)
            {
                CoinController.Instance.DropCoin(transform.position, CoinValue);
            }

            if(Random.value <= BenzakDropRate)
            {
                BenzakController.Instance.DropBenzak(transform.position, BenzakToGive);
            }

            SFXManager.Instance.PlaySfxPitched(0);
        }
        else
        {
            SFXManager.Instance.PlaySfxPitched(1);
        }

        DamageNumberController.Instance.SpawnDamage(damageToTake, transform.position);
    }

    public void TakeDamage(float damageToTake, bool shouldKnockBack)
    {
        TakeDamage(damageToTake);

        if(shouldKnockBack == true)
        {
            _knockBackCounter = KnockBackTime;
        }

        Instantiate(Blood, transform.position, Quaternion.identity);
    }
}
