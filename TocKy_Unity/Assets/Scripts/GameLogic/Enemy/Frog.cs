using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    [SerializeField] private Transform m_transform;
    public override void Move()
    {
        this.m_rig.velocity = Vector2.right * m_speed;
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
