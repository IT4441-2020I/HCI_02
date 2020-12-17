using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameData : MonoBehaviour
{
    public class TemplateText {
        public string VanBan;
        public string TocKy;
        public TemplateText(string vanBan, string tocKy) {
            this.VanBan = vanBan;
            this.TocKy = tocKy;
        }
    }
    
    public static GameObject SceneDataCurrent;
    private static GameData m_instance;
    public List<TemplateText> CurrentData;
    public static GameData Instance {
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
    private void Start() {
        this.CurrentData = new List<TemplateText>();
        UIGame.Instance.m_popupAnimator.SetBool("isOpened", false);
        this.ReadData(1);
    }
    public void ReadData(int index) {
        var data = Resources.Load<TextAsset>("GameData/Data_" + index).text.Split('\r','\n');
        for (int i = 0; i < data.Length; i++)
        {
            if (!data[i].Equals("")) {
                var tmp = data[i].Split('\t');
                CurrentData.Add(new TemplateText(tmp[0], tmp[1]));
            }
        }
    }
    private Color m_normalPopupColor = Color.white;
    private Color m_wrongPopupColor = new Color(1.0f, 0.3726415f, 0.3726415f, 0.85f);
    public string ReadText(int index) {
        // m_popupTemPlateTyping.gameObject.SetActive(true);
        UIGame.Instance.m_popupAnimator.SetBool("isOpened", true);
        UIGame.Instance.m_popupTemPlateTyping.color = m_normalPopupColor;
        UIGame.Instance.m_templateVB.text = CurrentData[index].VanBan;
        UIGame.Instance.m_templateTocKy.text = CurrentData[index].TocKy;
        return CurrentData[index].TocKy;
    }
    public void AlertWrongTyping() {
        UIGame.Instance.m_popupTemPlateTyping.color = m_wrongPopupColor;
        UIGame.Instance.m_popupAnimator.SetBool("isOpened", false);
    }
    public void AlertRightTyping() {
        UIGame.Instance.m_popupTemPlateTyping.color = m_normalPopupColor;
        UIGame.Instance.m_popupAnimator.SetBool("isOpened", false);
    }
}
