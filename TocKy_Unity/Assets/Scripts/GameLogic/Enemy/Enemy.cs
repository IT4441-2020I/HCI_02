using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float m_speed;
    protected float m_healthy;
    [SerializeField] protected Rigidbody2D m_rig;
    public abstract void Move();
    public abstract void Death();
}
