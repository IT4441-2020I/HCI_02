using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    None = 0,
    Idle = 1,
    MoveLeft = 2,
    MoveRight = 3,
    Jump = 4,
    Die = 5,
}
public class Player : MonoBehaviour
{
    private static Player m_instance;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody2D m_rig; 
    [SerializeField] private Transform m_transform;
    [SerializeField] private SpriteRenderer m_spR;
    public float Speed = -1;
    public float JumpForce = -1;
    [SerializeField] private bool isGrounded = true;
    public static Player Instance {
        get {
            return m_instance;
        }
    }
    private void Awake() {
        if (m_instance != null && m_instance != this) {
            Destroy(this.gameObject);
            return;
        }
        m_instance = this;
    }
    public void MoveLeft() {
        this.Move(-1);
    }
    public void MoveRight() {
        this.Move(1);
    }
    private void Move(float input) {
        if (!this.isGrounded) return;
        if (input > 0) {
            m_spR.flipX = false;
        }  
        else if (input < 0) {
            m_spR.flipX = true;
        }
        this.Speed = 1;
        m_animator.SetFloat("Speed", Speed);
        m_rig.velocity = new Vector2(4 * input, m_rig.velocity.y);
    }
    public void Jump() {
        if (this.isGrounded) {
            this.JumpForce = 1;
            m_animator.SetFloat("Force", JumpForce);
            m_rig.velocity = new Vector2 (4.0f, 8.0f);
            GameControler.Instance.ChangePlayerState(PlayerState.MoveRight);
        }
        
    }
    public void Attack() {
        this.JumpForce = 1;
        m_animator.SetFloat("Force", JumpForce);
        m_rig.velocity = new Vector2 (m_rig.velocity.x, 5f);
    }
    public void Idle() {
        this.Speed = -1;
        m_animator.SetFloat("Speed", Speed);
        m_rig.velocity = new Vector2(0, m_rig.velocity.y);
    }
    public void Die() {
        m_rig.velocity = new Vector2 (m_rig.velocity.x, 10f);
        this.GetComponent<Collider2D>().isTrigger = true;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag.Equals("Ground")) {
            this.isGrounded = true;
            this.JumpForce = -1;
            m_animator.SetFloat("Force", JumpForce);
        }
        if (other.collider.tag.Equals("WinPoint")) {
            StartCoroutine(GameControler.Instance.ChangePlayerStateHandleDelay(0, PlayerState.Idle));
            UIGame.Instance.GameWin();
        }
        
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.collider.tag.Equals("Ground")) {
            this.isGrounded = false;
        }
    }
}
