using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_transform;
    [SerializeField] private Vector3 m_offset;
    private Transform m_playerTransform;
    // Start is called before the first frame update
    private static CameraFollow s_instance;
    public static CameraFollow Instance {
        get {
            return s_instance;
        }
    }
    private void Awake() {
        if (s_instance != null && s_instance != this) {
            Destroy(this.gameObject);
            return;
        }
        s_instance = this;
    }
    public void Begin()
    {
        m_playerTransform = Player.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerTransform != null) {
            this.m_transform.position = m_playerTransform.position + m_offset;
            this.m_transform.position = new Vector3 (m_transform.position.x, 0 , -10);
        }
        
    }
}
