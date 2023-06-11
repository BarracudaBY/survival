using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBossTuretController : MonoBehaviour
{
    public static EnemyBossTuretController Instance;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private float _range;

    public GameObject AlarmLigt;
    public BulletRocket _bulletRocket;
    private bool _detected = false;
    private Vector2 _direction;

    public List<GameObject> Shields;

    public float Damage;
    public float HitWaitTime;
    public float MaxHealth;
    public float CurrentHealth;
    public float KnockBackTimer;

    [SerializeField] private Transform _target;
    [SerializeField] private Transform _gun;

    public int ExpToGive = 1;
    public int CoinValue = 1;
    public int BenzakToGive = 1;
    public float CoinDropRate = .5f;
    public float BenzakDropRate = 0.1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //_target = PlayerHealthController.Instance.transform;
        CurrentHealth = MaxHealth;
        _healthSlider.maxValue = MaxHealth;
        _healthSlider.value = CurrentHealth;
    }

    private void Update()
    {
        Vector2 targetPosition = _target.position;
        _direction = targetPosition - (Vector2)transform.position;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position,_direction,_range);
        if (rayInfo)
        {
            if(rayInfo.collider.gameObject.tag == "Player")
            {
                if(_detected == false)
                {
                    _detected = true;
                    AlarmLigt.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            else
            {
                if (_detected == true)
                {
                    _detected = false;
                    AlarmLigt.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
        }

        if (_detected)
        {
            _gun.transform.up = _direction;
        }
        if(PlayerHealthController.Instance.gameObject.activeSelf == true)
        {
            if(KnockBackTimer  > 0)
            {
                KnockBackTimer -= Time.deltaTime;

                if (_moveSpeed > 0)
                {
                    _moveSpeed = -_moveSpeed * 2f;
                }

                if (KnockBackTimer <= 0)
                {
                    _moveSpeed = Mathf.Abs(_moveSpeed * 0.5f);
                }
            }
        }
    }

    public void TakeDamage(float  damage)
    {
        if(Shields.Count <= 0)
        {
            CurrentHealth -= damage;

            if(CurrentHealth <= 0)
            {
                Destroy(this);
            }

        }
        
    }

    public void TakeDamage()
    {

    }

    private void Shoot()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
