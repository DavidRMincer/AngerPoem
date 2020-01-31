using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Character_Script : MonoBehaviour
{
    public float            walkSpeed;

    internal float          _currentSpeed;
    internal Rigidbody2D    _rb;

    internal virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentSpeed = walkSpeed;
    }

    public void Move(float x, float y)
    {
        _rb.velocity = new Vector2(x, y).normalized * _currentSpeed * Time.deltaTime;
    }

    public void SetRBType(RigidbodyType2D rbType)
    {
        _rb.bodyType = rbType;
    }
}
