using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : Enemy
{
    [SerializeField] private SpriteRenderer m_spR;
    [SerializeField] private Transform m_transform;
    private void Start() {
        this.m_speed = -1.5f;
        InvokeRepeating("FlipX", 3.0f, 3.0f);
    }
    private void Update() {
        this.Move();
    }
    public override void Move()
    {
        this.m_rig.velocity = Vector2.right * m_speed;
    }
    private void FlipX() {
        m_spR.flipX = !m_spR.flipX;
        this.m_speed = -m_speed;
    }
    public override void Death()
    {
        FxManagerment.Instance.GetFx("FxEnemyDeath", this.m_transform.position);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null) {
            // player.Attack();
            this.Death();
        }
    }
}
