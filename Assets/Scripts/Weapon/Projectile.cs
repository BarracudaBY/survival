using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float MoveSpeed;

    private void Update()
    {
        transform.position += transform.up * MoveSpeed * Time.deltaTime;
    }
}
