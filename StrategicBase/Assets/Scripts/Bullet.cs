using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : DamageItem
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.forward * _speed;
    }
}
