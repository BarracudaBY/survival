using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneemyAnimation : MonoBehaviour
{
    [SerializeField] private Transform _sprite;
    [SerializeField] private float _speed;
    [SerializeField] private float _minSize, _maxSize;

    private float _activeSize;

    private void Start()
    {
        _activeSize = _maxSize;

        _speed = _speed * Random.Range(0.75f, 1.25f);
    }

    private void Update()
    {
        _sprite.localScale = Vector3.MoveTowards(_sprite.localScale, Vector3.one * _activeSize, _speed * Time.deltaTime);

        if(_sprite.localScale.x == _activeSize)
        {
            if(_activeSize == _maxSize)
            {
                _activeSize = _minSize;
            }
            else
            {
                _activeSize = _maxSize;
            }
        }
    }
}
