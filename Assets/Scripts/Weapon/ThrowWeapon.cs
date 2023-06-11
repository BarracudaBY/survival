using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    public float trowPower;
    public Rigidbody2D Rigidbody;
    public float RotateSpeed;

    private void Start()
    {
        Rigidbody.velocity = new Vector2(Random.Range(-trowPower, trowPower), trowPower);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (RotateSpeed * 360f * Time.deltaTime * Mathf.Sign(Rigidbody.velocity.x)));
    }
}
