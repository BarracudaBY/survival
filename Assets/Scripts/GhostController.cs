using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public GameObject ghostPrefub;

    public float Delay = 1.0f;
    public float DestroyTime = 0.1f;
    public Color Color;
    public Material Material = null;

    private float _delta = 0f;

    PlayerController _player;
    SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(_delta > 0)
        {
            _delta -= Time.deltaTime;
        }
        else
        {
            _delta = Delay;
            SpawnGhost();
        }
    }

    public void SpawnGhost()
    {
        GameObject ghostObj = Instantiate(ghostPrefub, transform.position, transform.rotation);
        ghostObj.transform.localScale = _player.transform.localScale;
        Destroy(ghostObj, DestroyTime);
        _spriteRenderer = ghostObj.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _player.spriteRenderer.sprite;
        //_spriteRenderer.sprite = PlayerController.Instance.SpriteRenderer.sprite;
        _spriteRenderer.color = Color;

        if(Material != null)
        {
            _spriteRenderer.material = Material;
        }
    }

}
