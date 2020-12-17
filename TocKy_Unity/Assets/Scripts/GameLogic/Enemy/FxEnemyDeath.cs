using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxEnemyDeath : MonoBehaviour
{
    [SerializeField] private float m_timeLife;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("TurnOff", m_timeLife);
    }

    private void TurnOff() {
        Destroy(this.gameObject);
    }
}
