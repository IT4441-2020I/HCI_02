using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void ExcutionMethod();
public class GameControler : MonoBehaviour
{
    [SerializeField] private PlayerState m_playerState; 
    private float m_moveInput;
    private ExcutionMethod m_excutionMethod;
    private static GameControler m_instance;
    public static GameControler Instance {
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
    // Start is called before the first frame update
    void Start()
    {
        this.ChangePlayerState(PlayerState.MoveRight);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.m_excutionMethod != null) {
            this.m_excutionMethod();
        }
        // m_moveInput = Input.GetAxis("Horizontal");
        // Debug.Log("m_moveInput" + m_moveInput);
        // if ( m_moveInput != 0) {
        //     m_playerState = PlayerState.Move;
        //     Player.Instance.Move(m_moveInput);
        // } else {
        //     if (!Input.GetKey(KeyCode.W) && !m_playerState.Equals(PlayerState.Idle)) {
        //         Player.Instance.Idle();
        //         m_playerState = PlayerState.Idle;
        //         Debug.Log("Dung cmnim");
        //     }
        // }
        // if (Input.GetKey(KeyCode.W)) {
        //     m_playerState = PlayerState.Jump;
        //     Player.Instance.Jump();
        // }
    }
    public void ChangePlayerState(PlayerState playerState) {
        switch (playerState)
        {
            case PlayerState.Idle:
                m_playerState = PlayerState.Idle;
                m_excutionMethod = Player.Instance.Idle;
                break;
            case PlayerState.MoveRight:
                m_playerState = PlayerState.MoveRight;
                m_excutionMethod = Player.Instance.MoveRight;
                break;
            case PlayerState.MoveLeft:
                m_playerState = PlayerState.MoveLeft;
                m_excutionMethod = Player.Instance.MoveLeft;
                break;
            case PlayerState.Jump:
                m_playerState = PlayerState.Jump;
                m_excutionMethod = Player.Instance.Jump;
                break;
            case PlayerState.Die:
                m_playerState = PlayerState.Die;
                m_excutionMethod = Player.Instance.Die;
                break;
        }
    }
    public IEnumerator ChangePlayerStateHandleDelay(float timeDelay, PlayerState playerState) {
        yield return new WaitForSeconds(timeDelay);
        GameControler.Instance.ChangePlayerState(playerState);
    }
    public PlayerState PlayerState {
        get {
            return m_playerState;
        }
        set {
            m_playerState = value;
        }
    }
    
}
