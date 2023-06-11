using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public int CoinAmout = 1;
    public float MoveSpeed;
    public float TimeBetweenCheck = 0.2f;


    private bool _movingToPlayer;
    private float _checkCounter;

    private PlayerController _player;

    private void Start()
    {
        _player = PlayerController.Instance;
    }

    private void Update()
    {
        if (_movingToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, MoveSpeed * Time.deltaTime);
        }
        else
        {
            _checkCounter -= Time.deltaTime;
            if (_checkCounter <= 0)
            {
                _checkCounter = TimeBetweenCheck;
                if (Vector3.Distance(transform.position, _player.transform.position) < _player.PickUpRange)
                {
                    _movingToPlayer = true;
                    MoveSpeed += _player.MoveSpeed;
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CoinController.Instance.AddCoin(CoinAmout);

            Destroy(gameObject);
        }
    }
}
