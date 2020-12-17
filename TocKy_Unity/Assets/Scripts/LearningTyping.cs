using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TemplateTyping {
    public string VanBan;
    public string TocKy;
    public TemplateTyping(string vanBan, string tocKy) {
        this.VanBan = vanBan;
        this.TocKy = tocKy;
    }
} 
public class LearningTyping : MonoBehaviour
{
    private TemplateTyping[] m_templateTyping;
    private List<TemplateTyping> m_templateTypingAmTiet;
    private List<TemplateTyping> m_templateTypingTu;
    private List<TemplateTyping> m_templateTypingCau;
    private int m_valueDropMenuOptionTyping;
    [SerializeField] private Text m_templateVanBanText;
    [SerializeField] private Text m_templateTocKyText;
    [SerializeField] private Dropdown m_dropMenu;
    // Start is called before the first frame update
    void Start()
    {
        if (m_templateTypingAmTiet == null) {
            m_templateTypingAmTiet = new List<TemplateTyping>();
            TextAsset MauAmTietFile = Resources.Load("MauAmTiet") as TextAsset;
            var arrayTmp = MauAmTietFile.text.Split('\r','\n');
            for (int i = 0; i < arrayTmp.Length; i++)
            {
                if (!arrayTmp[i].Equals("")) {
                    var tmp = arrayTmp[i].Split('\t');
                    m_templateTypingAmTiet.Add(new TemplateTyping(tmp[0], tmp[1]));
                }
            }
        }
        m_valueDropMenuOptionTyping = 0;
        m_templateTyping = m_templateTypingAmTiet.ToArray();
    }

    public void HandleDropMenuOptionTyping() {
        m_valueDropMenuOptionTyping = m_dropMenu.value;
        switch (m_valueDropMenuOptionTyping) {
            case 1:
                if (m_templateTypingTu == null) {
                    m_templateTypingTu = new List<TemplateTyping>();
                    TextAsset MauTuFile = Resources.Load("MauTu") as TextAsset;
                    var arrayTmp = MauTuFile.text.Split('\r','\n');
                    for (int i = 0; i < arrayTmp.Length; i++)
                    {
                        if (!arrayTmp[i].Equals("")) {
                            var tmp = arrayTmp[i].Split('\t');
                            Debug.Log(tmp.Length);
                            m_templateTypingTu.Add(new TemplateTyping(tmp[0], tmp[1]));
                        }
                    }
                    
                }
                m_templateTyping = m_templateTypingTu.ToArray();
                break;
            case 2:
                if (m_templateTypingCau == null) {
                    m_templateTypingCau = new List<TemplateTyping>();
                    TextAsset MauCauFile = Resources.Load("MauCau") as TextAsset;
                    var arrayTmp = MauCauFile.text.Split('\r','\n');
                    for (int i = 0; i < arrayTmp.Length; i++)
                    {
                        if (!arrayTmp[i].Equals("")) {
                            var tmp = arrayTmp[i].Split('\t');
                            m_templateTypingCau.Add(new TemplateTyping(tmp[0], tmp[1]));
                        }
                    }
                }
                m_templateTyping = m_templateTypingCau.ToArray();
                break;
        }
    }
    private int m_index2 = -1;
    private int m_index1 = -1;
    private int m_index0 = -1;

    public void OnTaoMauGo() {
        if (m_valueDropMenuOptionTyping == 0) {
            m_index2 += 7;
            if (m_index2>=m_templateTyping.Length) m_index2 = 6;
            m_templateVanBanText.text = "";
            m_templateTocKyText.text = "";
            if (m_index2 < m_templateTyping.Length)
                for (int i = m_index2-6; i <= m_index2; i++)
                {
                    m_templateVanBanText.text +=  m_templateTyping[i].VanBan + " ";
                    m_templateTocKyText.text += m_templateTyping[i].TocKy + " ";
                }
        } 
        else if (m_valueDropMenuOptionTyping == 1) {
            m_index1 += 7;
            if (m_index1>=m_templateTyping.Length) m_index1 = 6;
            m_templateVanBanText.text = "";
            m_templateTocKyText.text = "";
            if (m_index1 < m_templateTyping.Length)
                for (int i = m_index1-6; i <= m_index1; i++)
                {
                    m_templateVanBanText.text +=  m_templateTyping[i].VanBan + " ";
                    m_templateTocKyText.text += m_templateTyping[i].TocKy + " ";
                }
        } 
        else if (m_valueDropMenuOptionTyping == 2) {
            m_index0 += 1;
            if (m_index0>=m_templateTyping.Length) m_index0 = 0;
            if (m_index0 < m_templateTyping.Length) {
                m_templateVanBanText.text = m_templateTyping[m_index0].VanBan;
                m_templateTocKyText.text = m_templateTyping[m_index0].TocKy;
            } 
        }
    }
}
