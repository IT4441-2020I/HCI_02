using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [SerializeField] private Image[] m_selectedImage;
    private int m_index = -1;
    private void Start() {
        HandleClick(0);
    }
    public void OnGoTocKy() {
        HandleClick(0);
    }
    public void OnChiTietBoGo() {
        HandleClick(1);
    }
    public void OnBoTuDien() {
        HandleClick(2);
    }
    public void OnHuongDanSuDung() {
        HandleClick(3);
    }
    public void OnTrangChuTocKy() {
        HandleClick(4);
    }
    private void HandleClick(int index) {
        if (m_index != index) {
            m_index = index;
            for (int i = 0; i < m_selectedImage.Length; i++)
            {
                if (i == m_index) m_selectedImage[i].gameObject.SetActive(true);
                else m_selectedImage[i].gameObject.SetActive(false);
            }
        } 
    }
}
