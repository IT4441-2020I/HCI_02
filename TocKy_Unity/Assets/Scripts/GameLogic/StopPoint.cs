using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPoint : MonoBehaviour
{
    [SerializeField] private int m_index;
    public static string CodeTocKy = "";
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Player")) {
            GameControler.Instance.ChangePlayerState(PlayerState.Idle);
            CodeTocKy = GameData.Instance.ReadText(m_index);
            Debug.Log("Nhay");
            this.StartCoroutine(Keyboard.Instance.NonPlayer());
        }
    }
}
